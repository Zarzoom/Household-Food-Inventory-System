using System.Net.Http.Json;
using MySqlConnector;
using Dapper;
using System.Text;
using System.Text.Json;
using InventoryInCSharpAPI.Models;

namespace InventoryInCSharp.IntegrationTests.ItemTests;

public class ItemTests
{
    protected static readonly HttpClient client = new HttpClient();

    public void InsertLogin()
    {
        User testUser = new User();
        testUser.userName = "testUser";
        testUser.password = 1;
        InsertMethods.LoginInsertDirectlyToDatabase(testUser);
        
        User testUser2 = new User();
        testUser.userName = "testUser2";
        testUser.password = 2;
        InsertMethods.LoginInsertDirectlyToDatabase(testUser);
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
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();
        InsertLogin();
        Task.Delay(1000).Wait();
        try
        {

            var item = new Item();
            item.genericName = "Test";
            item.brand = "Best Test";
            item.size = "1 question";
            item.price = 7.88F;
            item.password = 1;

            var content = new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/Item", content);
            httpResponse.Wait();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(1000).Wait(); 

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
        expectedItem.password = 1;

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

}

public class WhenItemsAreInsertedDirectlyToDatabaseAndSearchedForByPrimaryKey : ItemTests
{
    private static readonly HttpClient client = new HttpClient();
    private Item actualItem = new Item();
    private Item expectedItem = new Item();

    [OneTimeSetUp]
    public void Setup()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();

        InsertLogin();
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
        expectedItem.password = 1;
        expectedItem.itemID = 2;
    }

    public void InsertItemsForTesting()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        testItem1.password = 1;
        //testItem1.itemID = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.password = 1;
        //testItem2.itemID = 2;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.password = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem3);
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

    [Test]
    public void ThenTheCorrectPrimaryKeyIsFound()
    {
        Assert.AreEqual(expectedItem.itemID, actualItem.itemID);
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
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();

        InsertLogin();
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
        expectedItem.password = 1;
        expectedItem.itemID = 2;
    }

    public void InsertItemsForTesting()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        testItem1.password = 1;

        InsertMethods.ItemInsertUsingAPI(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.password = 1;

        InsertMethods.ItemInsertUsingAPI(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.password = 1;

        InsertMethods.ItemInsertUsingAPI(testItem3);
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
    [Test]
    public void ThenTheCorrectPrimaryKeyIsFound()
    {
        Assert.AreEqual(expectedItem.itemID, actualItem.itemID);
    }

}

public class WhenItemsAreInsertedThroughTheAPIToDatabaseAndSearchedForWithGenericNameAndBrandContentsSearch : ItemTests
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
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();

        InsertLogin();
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
        GetExpectedItemString();
    }

    public void GetExpectedItemString()
    {
        expectedItem1.genericName = "Generic Name Test 1";
        expectedItem1.brand = "Brand Test 1";
        expectedItem1.price = 1.99F;
        expectedItem1.size = "1 Test";
        expectedItem1.password = 0;
        expectedItem1.itemID = 1;
        expectedItemList.Add(expectedItem1);

        expectedItem2.genericName = "Generic Name Test 2";
        expectedItem2.brand = "Brand Test 2";
        expectedItem2.price = 1.99F;
        expectedItem2.size = "2 Test";
        expectedItem2.password = 0;
        expectedItem2.itemID = 2;

        expectedItemList.Add(expectedItem2);

        expectedItem3.genericName = "Generic Name Test 3";
        expectedItem3.brand = "Brand Test 3";
        expectedItem3.price = 1.99F;
        expectedItem3.size = "3 Test";
        expectedItem3.password = 0;
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
        testItem1.password = 1;


        InsertMethods.ItemInsertUsingAPI(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.password = 1;
        //testItem2.itemID = 2;

        InsertMethods.ItemInsertUsingAPI(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.password = 1;

        InsertMethods.ItemInsertUsingAPI(testItem3);

        Item testItem4 = new Item();
        testItem4.genericName = "Should Not Be Found";
        testItem4.brand = "should not be found";
        testItem4.price = 9.99F;
        testItem4.size = "4 not found";
        testItem4.password = 1;

        InsertMethods.ItemInsertUsingAPI(testItem4);
    }

    //Execute
    [Test]
    public void ThenTheCorrectItemsAreFound()
    {
        Assert.AreEqual(expectedItemString, actualItemString);
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
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();
        InsertLogin();
        InsertItemsForUpdate();
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

    public void InsertItemsForUpdate()
    {
        Item itemToUpdate = new Item();
        itemToUpdate.brand = "Test Failure";
        itemToUpdate.price = 99999.99F;
        itemToUpdate.genericName = "Failed Test";
        itemToUpdate.size = "1 failed test";
        itemToUpdate.password = 1;
        InsertMethods.ItemInsertDirectlyToDatabase(itemToUpdate);

        Item testItem1 = new Item();
        testItem1.brand = "Wrong Item 1";
        testItem1.price = 8888.88F;
        testItem1.genericName = "Wrong Item";
        testItem1.size = "1 wrong item";
        testItem1.password = 1;
        InsertMethods.ItemInsertDirectlyToDatabase(testItem1);
        
        Item testItem2 = new Item();
        testItem2.brand = "Wrong Item 2";
        testItem2.price = 8888.88F;
        testItem2.genericName = "Wrong Item";
        testItem2.size = "2 wrong item";
        testItem2.password = 1;
        InsertMethods.ItemInsertDirectlyToDatabase(testItem2);
    }
    public Item GetActual()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT GenericName, Brand, Price, Size, ItemID FROM ItemList WHERE ItemID = 1";
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
        expectedItem.itemID = 1;

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

    [Test]
    public void ThenItemIDStayedTheSame()
    {
        Assert.AreEqual(expectedItem.itemID, actualItem.itemID);
    }

}

[TestFixture]
public class WhenItemsAreDeleted : ItemTests
{
    private static readonly HttpClient client = new HttpClient();
    private String actualItemString;
    private Item expectedItem1 = new Item();
    private Item expectedItem2 = new Item();
    private Item expectedItem3 = new Item();
    private List<Item> expectedItemList = new List<Item>();
    private String expectedItemString;
    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();

        InsertLogin();
        InsertItemsForTesting();
        try
        {
            using Task<HttpResponseMessage> httpResponse = client.PutAsync("http://localhost:8000/api/Item/deleteItem/2", null);
            httpResponse.Wait();
        }
        catch(Exception e)
        {
            throw;
        }
        Task.Delay(1000).Wait();
        GetActualItemList();
        Task.Delay(1000).Wait();
        GetExpectedItemList();
    }

    public void InsertItemsForTesting()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        testItem1.password = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.password = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.password = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem3);
    }

    public void GetActualItemList()
    {
        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Item");
            httpResponse.Wait();
            var result = httpResponse.Result.Content.ReadAsStringAsync();
            result.Wait();
            actualItemString = result.Result;

        }
        catch (Exception e)
        {
            throw;
        }

    }
    public void GetExpectedItemList()
    {
        expectedItem1.genericName = "Generic Name Test 1";
        expectedItem1.brand = "Brand Test 1";
        expectedItem1.price = 1.99F;
        expectedItem1.size = "1 Test";
        expectedItem1.itemID = 1;
        expectedItem1.password = 1;
        expectedItemList.Add(expectedItem1);

        expectedItem3.genericName = "Generic Name Test 3";
        expectedItem3.brand = "Brand Test 3";
        expectedItem3.price = 1.99F;
        expectedItem3.size = "3 Test";
        expectedItem3.itemID = 3;
        expectedItem3.password = 1;

        expectedItemList.Add(expectedItem3);

        expectedItemString = JsonSerializer.Serialize(expectedItemList);
    }

    [Test]
    public void ThenTheCorrectItemHasBeenDeleted()
    {
        Assert.AreEqual(actualItemString, expectedItemString);
    }
}

public class WhenItemListIsQueriedForAllItems : ItemTests
{
    private List<Item> expectedItemList = new List<Item>();
    private String expectedItemString;
    private String actualItemString;

    
    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();

        InsertLogin();
        ItemInsert();


        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Item");
            httpResponse.Wait();
            var result = httpResponse.Result.Content.ReadAsStringAsync();
            result.Wait();
            actualItemString = result.Result;
        }
        catch (Exception e)
        {
            throw;
        }
        
        CreateExpected();
    }
    
    public void ItemInsert()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        testItem1.password = 1;
        //testItem1.itemID = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.password = 1;
        //testItem2.itemID = 2;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.password = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem3);
    }

    public void CreateExpected()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        testItem1.password = 1;
        testItem1.itemID = 1;

        expectedItemList.Add(testItem1);


        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.password = 1;
        testItem2.itemID = 2;

        expectedItemList.Add(testItem2);


        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.password = 1;
        testItem3.itemID = 3;

       expectedItemList.Add(testItem3);

       expectedItemString = JsonSerializer.Serialize(expectedItemList);
    }

    [Test]
    public void ThenAllPantriesHaveBeenReturned()
    {
        Assert.AreEqual(expectedItemString, actualItemString);
    }
}

public class WhenItemsAreInsertedThroughTheAPIToDatabaseAndSearchedForWithPassword : ItemTests
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
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();

        InsertLogin();
        InsertItemsForTesting();

        try
        {
            Task.Delay(1000).Wait();
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Item/userSearch/1");
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
        GetExpectedItemString();
    }

    public void GetExpectedItemString()
    {


        expectedItem2.genericName = "Generic Name Test 2";
        expectedItem2.brand = "Brand Test 2";
        expectedItem2.price = 1.99F;
        expectedItem2.size = "2 Test";
        expectedItem2.password = 0;
        expectedItem2.itemID = 2;

        expectedItemList.Add(expectedItem2);

        expectedItem3.genericName = "Generic Name Test 3";
        expectedItem3.brand = "Brand Test 3";
        expectedItem3.price = 1.99F;
        expectedItem3.size = "3 Test";
        expectedItem3.password = 0;
        expectedItem3.itemID = 3;

        expectedItemList.Add(expectedItem3);

        expectedItemString = JsonSerializer.Serialize(expectedItemList);
    }

    public void InsertItemsForTesting()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Should Not Be Found 1";
        testItem1.brand = "should not be found 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 not found";
        testItem1.password = 2;


        InsertMethods.ItemInsertUsingAPI(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.password = 1;
        //testItem2.itemID = 2;

        InsertMethods.ItemInsertUsingAPI(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.password = 1;

        InsertMethods.ItemInsertUsingAPI(testItem3);

        Item testItem4 = new Item();
        testItem4.genericName = "Should Not Be Found 4";
        testItem4.brand = "should not be found 4";
        testItem4.price = 9.99F;
        testItem4.size = "4 not found";
        testItem4.password = 2;

        InsertMethods.ItemInsertUsingAPI(testItem4);
    }

    //Execute
    [Test]
    public void ThenTheCorrectItemsAreFound()
    {
        Assert.AreEqual(expectedItemString, actualItemString);
    }


}