using System.Net.Http.Json;
using MySqlConnector;
using Dapper;
using System.Text;
using System.Text.Json;
using InventoryInCSharpAPI.Models;

namespace InventoryInCSharp.IntegrationTests;

public class ItemTests
{
    protected static readonly HttpClient client = new HttpClient();

    public void InsertDirectlyToDatabase(Item newItem)
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO ItemList (GenericName, Brand, Price, Size) VALUES (@genericName, @brand, @price, @size)";
            var actual = connection.ExecuteAsync(sql, newItem);
            actual.Wait();
        }
    }

    public void InsertUsingAPI(Item newItem)
    {
        try
        {

            var content = new StringContent(JsonSerializer.Serialize(newItem), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/Item", content);
            httpResponse.Wait();
            Task.Delay(1000).Wait();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

[TestFixture]
public class WhenItemIsInserted : ItemTests
{
    private Item actualItem = new Item();
    private Item expectedItem = new Item();

    [OneTimeSetUp]
    public void Setup()
    {
        try
        {

            var item = new Item();
            item.genericName = "Test";
            item.brand = "Best Test";
            item.size = "1 question";
            item.price = 7.88F;

            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/Item", content);
            httpResponse.Wait();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        actualItem = GetActual();

        expectedItem = CreateExpected();
    }


    public Item GetActual()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT * FROM ItemList";
            var actual = connection.Query<Item>(sql);
            return actual.Single();
        }
    }

    public Item CreateExpected()
    {
        expectedItem.genericName = "Test";
        expectedItem.brand = "Best Test";
        expectedItem.size = "1 question";
        expectedItem.price = 7.88F;

        return expectedItem;
    }
    
    //Execute
    [Test]
    public void ThenItemsBrandIsCorrect()
    {
        Assert.AreEqual(expectedItem.brand, actualItem.brand);
    }

    [Test]
    public void ThenItemsGenericNameIsCorrect()
    {
        Assert.AreEqual(expectedItem.genericName, actualItem.genericName);

    }

    [Test]
    public void ThenItemsPriceIsCorrect()
    {
        Assert.AreEqual(expectedItem.price, actualItem.price);
    }
    [Test]
    public void itemsSizeCorrect()
    {
        Assert.AreEqual(expectedItem.size, actualItem.size);
    }


    [OneTimeTearDown]
    public void Destruction()
    {
        using Task<HttpResponseMessage> httpResponse = client.PutAsync($"http://localhost:8000/api/Item/deleteItem/{actualItem.itemID}", null);
        httpResponse.Wait();

        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE ItemList AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
}

public class WhenItemsAreInsertedDirectlyToDatabaseAndSearchedForByPrimaryKey : ItemTests
{
    private static readonly HttpClient client = new HttpClient();
    private Item actualItem = new Item();
    private Item expectedItem = new Item();

    [OneTimeSetUp]
    public void Setup()
    {
        InsertItemsForTesting();

        try
        {

            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Item/2");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadFromJsonAsync<Item>();
            results.Wait();
            actualItem = results.Result;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        GetExpected();
    }

    public void GetExpected()
    {
        expectedItem.genericName = "Generic Name Test 2";
        expectedItem.brand = "Brand Test 2";
        expectedItem.price = 1.99F;
        expectedItem.size = "2 Test";
    }

    public void InsertItemsForTesting()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        //testItem1.itemID = 1;

        InsertDirectlyToDatabase(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        //testItem2.itemID = 2;

        InsertDirectlyToDatabase(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";

        InsertDirectlyToDatabase(testItem3);
    }

    //Execute
    [Test]
    public void ThenTheCorrectGenericNameIsFound()
    {
        Assert.AreEqual(expectedItem.genericName, actualItem.genericName);
    }

    [Test]
    public void ThenTheCorrectBrandIsFound()
    {
        Assert.AreEqual(expectedItem.brand, actualItem.brand);
    }

    [Test]
    public void ThenTheCorrectPriceIsFound()
    {
        Assert.AreEqual(expectedItem.price, actualItem.price);
    }

    [Test]
    public void ThenTheCorrectSizeIsFound()
    {
        Assert.AreEqual(expectedItem.size, actualItem.size);
    }


    [OneTimeTearDown]
    public void Destruction()
    {

        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/1", null).Wait();
        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/2", null).Wait();
        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/3", null).Wait();

        Task.Delay(1000).Wait();

        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE ItemList AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
}

public class WhenItemsAreInsertedThroughTheAPIToDatabaseAndSearchedForByPrimaryKey : ItemTests
{
    private static readonly HttpClient client = new HttpClient();
    private Item actualItem = new Item();
    private Item expectedItem = new Item();

    [OneTimeSetUp]
    public void Setup()
    {
        InsertItemsForTesting();

        try
        {

            Task.Delay(1000).Wait();
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Item/2");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadFromJsonAsync<Item>();
            results.Wait();
            actualItem = results.Result;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        GetExpected();
    }

    public void GetExpected()
    {
        expectedItem.genericName = "Generic Name Test 2";
        expectedItem.brand = "Brand Test 2";
        expectedItem.price = 1.99F;
        expectedItem.size = "2 Test";
    }

    public void InsertItemsForTesting()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        //testItem1.itemID = 1;

        InsertUsingAPI(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        //testItem2.itemID = 2;

        InsertUsingAPI(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";

        InsertUsingAPI(testItem3);
    }

    //Execute
    [Test]
    public void ThenTheCorrectGenericNameIsFound()
    {
        Assert.AreEqual(expectedItem.genericName, actualItem.genericName);
    }

    [Test]
    public void ThenTheCorrectBrandIsFound()
    {
        Assert.AreEqual(expectedItem.brand, actualItem.brand);
    }

    [Test]
    public void ThenTheCorrectPriceIsFound()
    {
        Assert.AreEqual(expectedItem.price, actualItem.price);
    }

    [Test]
    public void ThenTheCorrectSizeIsFound()
    {
        Assert.AreEqual(expectedItem.size, actualItem.size);
    }


    [OneTimeTearDown]
    public void Destruction()
    {

        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/1", null).Wait();
        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/2", null).Wait();
        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/3", null).Wait();

        Task.Delay(1000).Wait();

        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE ItemList AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
}

public class WhenItemsAreInsertedThroughTheAPIToDatabaseAndSearchedForWihtGenericNameAndBrandContentsSearch : ItemTests
{
    private static readonly HttpClient client = new HttpClient();

    private String actualItemString;
    private Item expectedItem1 = new Item();
    private Item expectedItem2 = new Item();
    private Item expectedItem3 = new Item();
    private List<Item> expectedItemList = new List<Item>();
    private String expectedItemString;

    [OneTimeSetUp]
    public void Setup()
    {
        InsertItemsForTesting();

        try
        {
            Task.Delay(1000).Wait();
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Item/search/St");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadAsStringAsync();
            results.Wait();
            actualItemString = results.Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        GetExpectedItemList();
    }

    public void GetExpectedItemList()
    {
        expectedItem1.genericName = "Generic Name Test 1";
        expectedItem1.brand = "Brand Test 1";
        expectedItem1.price = 1.99F;
        expectedItem1.size = "1 Test";
        expectedItem1.itemID = 1;
        expectedItemList.Add(expectedItem1);

        expectedItem2.genericName = "Generic Name Test 2";
        expectedItem2.brand = "Brand Test 2";
        expectedItem2.price = 1.99F;
        expectedItem2.size = "2 Test";
        expectedItem2.itemID = 2;

        expectedItemList.Add(expectedItem2);

        expectedItem3.genericName = "Generic Name Test 3";
        expectedItem3.brand = "Brand Test 3";
        expectedItem3.price = 1.99F;
        expectedItem3.size = "3 Test";
        expectedItem3.itemID = 3;

        expectedItemList.Add(expectedItem3);

        expectedItemString = JsonSerializer.Serialize(expectedItemList);
    }

    public void InsertItemsForTesting()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";


        InsertUsingAPI(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        //testItem2.itemID = 2;

        InsertUsingAPI(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";

        InsertUsingAPI(testItem3);
    }

    //Execute
    [Test]
    public void ThenTheCorrectItemsAreFound()
    {
        Assert.AreEqual(expectedItemString, actualItemString);
    }

    [OneTimeTearDown]
    public void Destruction()
    {

        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/1", null).Wait();
        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/2", null).Wait();
        client.PutAsync($"http://localhost:8000/api/Item/deleteItem/3", null).Wait();
        Task.Delay(1000).Wait();


        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE ItemList AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
}

[TestFixture]
public class WhenItemsAreUpdated : ItemTests
{
    private Item actualItem = new Item();

    private Item expectedItem = new Item();

    [OneTimeSetUp]
    public void Setup()
    {
        InsertItemToUpdate();
        Task.Delay(1000).Wait();
        try
        {

            var updatedItem = new Item();
            updatedItem.genericName = "Test";
            updatedItem.brand = "Best Test";
            updatedItem.size = "1 question";
            updatedItem.price = 7.88F;
            updatedItem.itemID = 1;

            var content = new StringContent(JsonSerializer.Serialize(updatedItem), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PutAsync("http://localhost:8000/api/Item", content);
            httpResponse.Wait();

            Task.Delay(1000).Wait();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        actualItem = GetActual();
        Task.Delay(1000).Wait();

        expectedItem = CreateExpected();
    }

    public void InsertItemToUpdate()
    {
        Item itemToUpdate = new Item();
        itemToUpdate.brand = "Test Failure";
        itemToUpdate.price = 99999.99F;
        itemToUpdate.genericName = "Failed Test";
        itemToUpdate.size = "1 failed test";
        InsertDirectlyToDatabase(itemToUpdate);
    }
    public Item GetActual()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT * FROM ItemList";
            var actual = connection.Query<Item>(sql);
            return actual.Single();
        }
    }

    public Item CreateExpected()
    {
        expectedItem.genericName = "Test";
        expectedItem.brand = "Best Test";
        expectedItem.size = "1 question";
        expectedItem.price = 7.88F;

        return expectedItem;
    }

    //Execute

    [Test]
    public void ThenItemsBrandIsUpdated()
    {
        Assert.AreEqual(expectedItem.brand, actualItem.brand);
    }

    [Test]
    public void ThenItemsGenericNameIsUpdated()
    {
        Assert.AreEqual(expectedItem.genericName, actualItem.genericName);

    }

    [Test]
    public void ThenItemsPriceIsUpdated()
    {
        Assert.AreEqual(expectedItem.price, actualItem.price);
    }
    [Test]
    public void ThenItemsSizeUpdated()
    {
        Assert.AreEqual(expectedItem.size, actualItem.size);

    }


    [OneTimeTearDown]
    public void Destruction()
    {
        using Task<HttpResponseMessage> httpResponse = client.PutAsync($"http://localhost:8000/api/Item/deleteItem/{actualItem.itemID}", null);
        httpResponse.Wait();

        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE ItemList AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
}
