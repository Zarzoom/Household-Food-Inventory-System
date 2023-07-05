using System.Net.Http.Json;
using MySqlConnector;
using Dapper;
using System.Text;
using System.Text.Json;
using InventoryInCSharpAPI.Models;
namespace InventoryInCSharp.IntegrationTests.PantryContentsTests;

public class PantryContentsTests
{
    protected static readonly HttpClient client = new HttpClient();

    public void ItemInsert()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        //testItem1.itemID = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        //testItem2.itemID = 2;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        //testItem3.itemID = 3;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem3);
    }
    public void PantryInsert()
    {
        Pantry TestPantry1 = new Pantry();
        TestPantry1.pantryName = "Test Name 1";
        //TestPantry1.pantryID = 1;

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry1);

        Pantry TestPantry2 = new Pantry();
        TestPantry2.pantryName = "Test Name 2";
        //TestPantry2.pantryID = 2;

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry2);

        Pantry TestPantry3 = new Pantry();
        TestPantry3.pantryName = "Test Name 3";
        //TestPantry3.pantryID = 3;
        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry3);
    }
    public void PantryContentsInsert()
    {
        PantryContents testPantryContent1 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 1,
            quantity = 3,
            //pantryContentID = 1,

        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent1);

        PantryContents testPantryContent2 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 2,
            quantity = 3,
            //pantryContentID = 2,

        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent2);

        PantryContents testPantryContent3 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 3,
            quantity = 3,
            //pantryContentID = 3,
        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent3);

        PantryContents testPantryContent4 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 2,
            quantity = 3,
            //pantryContentID = 4,

        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent4);

        PantryContents testPantryContent5 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 3,
            quantity = 3,
            //pantryContentID = 5;

        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent5);
    }
}

[TestFixture]
public class WhenPantryContentIsInserted : PantryContentsTests
{
    private PantryContents expectedPantryContent = new PantryContents();
    private PantryContents actualPantryContent = new PantryContents();

    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();


        try
        {
            var newPantryContent = new PantryContents();
            newPantryContent.pcPantryID = 2;
            newPantryContent.pcItemID = 1;
            newPantryContent.quantity = 3;

            var content = new StringContent(JsonSerializer.Serialize(newPantryContent), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/PantryContents", content);
            httpResponse.Wait();

        }
        catch (Exception e)
        {
            throw;
        }
        Task.Delay(1000).Wait();
        actualPantryContent = GetActualPantryContents();
        CreateExpectedPantryContents();

    }

    public PantryContents GetActualPantryContents()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PCPantryID, PCItemID, Quantity, PantryContentID FROM PantryContents";
            var actual = connection.Query<PantryContents>(sql);
            return actual.Single();
        }
    }
    public void CreateExpectedPantryContents()
    {

        expectedPantryContent.pcPantryID = 2;
        expectedPantryContent.pcItemID = 1;
        expectedPantryContent.quantity = 3;
        expectedPantryContent.pantryContentID = 1;


    }

    [Test]
    public void ThenThePantryContentsIDIsCorrect()
    {
        Assert.AreEqual(expectedPantryContent.pantryContentID, actualPantryContent.pantryContentID);
    }

    [Test]
    public void ThenThePCPantryIDIsCorrect()
    {
        Assert.AreEqual(expectedPantryContent.pcPantryID, actualPantryContent.pcPantryID);
    }

    [Test]
    public void ThenThePCItemIDIsCorrect()
    {
        Assert.AreEqual(expectedPantryContent.pcItemID, actualPantryContent.pcItemID);
    }

    [Test]
    public void ThenTheQuantityIsCorrect()
    {
        Assert.AreEqual(expectedPantryContent.quantity, actualPantryContent.quantity);
    }
}

public class WhenTheItemsInAPantryAreFoundBySearchingPantryContentsWithThePantryID : PantryContentsTests
{
    private List<Item> expectedItemList = new List<Item>();
    private String expectedItemString;
    private String actualItemString;

    [OneTimeSetUp]
    public void SetUp()
    {

        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();
        PantryContentsInsert();

        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync($"http://localhost:8000/api/PantryContents/retrieveItemsFromPantry/{2}");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadAsStringAsync();
            results.Wait();
            actualItemString = results.Result;

        }
        catch (Exception e)
        {
            throw;
        }
        CreateExpected();

    }

    public void CreateExpected()
    {
        Item testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        testItem1.itemID = 1;
        testItem1.quantity = 3;

        expectedItemList.Add(testItem1);

        Item testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        testItem2.itemID = 2;
        testItem2.quantity = 3;

        expectedItemList.Add(testItem2);

        Item testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";
        testItem3.itemID = 3;
        testItem3.quantity = 3;

        expectedItemList.Add(testItem3);

        expectedItemString = JsonSerializer.Serialize(expectedItemList);
    }

    [Test]
    public void ThenAllItemsInThePantryAreFound()
    {
        Assert.AreEqual(expectedItemString, actualItemString);
    }
}

public class WhenPantryContentsAreSearchedForByPCPantryID : PantryContentsTests
{
    private List<PantryContents> expectedPantryContentsList = new List<PantryContents>();
    private String expectedPantryContentsString;
    private String actualPantryContentsString;

    [OneTimeSetUp]
    public void SetUp()
    {

        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();
        PantryContentsInsert();

        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync($"http://localhost:8000/api/PantryContents/{2}");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadAsStringAsync();
            results.Wait();
            actualPantryContentsString = results.Result;

        }
        catch (Exception e)
        {
            throw;
        }
        CreateExpected();

    }

    public void CreateExpected()
    {
        PantryContents testPantryContent1 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 1,
            quantity = 3,
            pantryContentID = 1,

        };

        expectedPantryContentsList.Add(testPantryContent1);

        PantryContents testPantryContent2 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 2,
            quantity = 3,
            pantryContentID = 2,

        };
        expectedPantryContentsList.Add(testPantryContent2);

        PantryContents testPantryContent3 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 3,
            quantity = 3,
            pantryContentID = 3,
        };
        expectedPantryContentsList.Add(testPantryContent3);

        expectedPantryContentsString = JsonSerializer.Serialize(expectedPantryContentsList);
    }

    [Test]
    public void ThenAllPantryContentsInThePantryAreFound()
    {
        Assert.AreEqual(expectedPantryContentsString, actualPantryContentsString);
    }
}

public class WhenPantryContentsAreSearchedForByPCItemID : PantryContentsTests
{
    private List<PantryItem> expectedPantryItemList = new List<PantryItem>();
    private String expectedPantryItemString;
    private String actualPantryItemString;

    [OneTimeSetUp]
    public void SetUp()
    {

        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();
        PantryContentsInsert();

        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync($"http://localhost:8000/api/PantryContents/retrieveItemLocation/{2}");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadAsStringAsync();
            results.Wait();
            actualPantryItemString = results.Result;

        }
        catch (Exception e)
        {
            throw;
        }
        CreateExpected();

    }

    public void CreateExpected()
    {

        PantryItem TestPantryItem1 = new PantryItem();
        TestPantryItem1.pantryName = "Test Name 2";
        TestPantryItem1.pantryContentID = 2;
        TestPantryItem1.quantity = 3;
        expectedPantryItemList.Add(TestPantryItem1);

        PantryItem TestPantryItem2 = new PantryItem();
        TestPantryItem2.pantryName = "Test Name 3";
        TestPantryItem2.pantryContentID = 4;
        TestPantryItem2.quantity = 3;
        expectedPantryItemList.Add(TestPantryItem2);

        expectedPantryItemString = JsonSerializer.Serialize(expectedPantryItemList);
    }

    [Test]
    public void ThenAllPantryContentsContainingTheItemAreFound()
    {
        Assert.AreEqual(expectedPantryItemString, actualPantryItemString);
    }
}

public class WhenPantryContentsAreSearchedWithPCItemIDAndPCPantryID : PantryContentsTests
{
    private PantryContents expectedPantryContents = new PantryContents();
    private PantryContents actualPantryContents = new PantryContents();


    [OneTimeSetUp]
    public void SetUp()
    {

        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();
        PantryContentsInsert();

        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync($"http://localhost:8000/api/PantryContents/{2}/{3}");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadFromJsonAsync<PantryContents>();
            results.Wait();
            actualPantryContents = results.Result;
        }
        catch (Exception e)
        {
            throw;
        }
        CreateExpected();

    }

    public void CreateExpected()
    {
        expectedPantryContents.pcPantryID = 2;
        expectedPantryContents.pcItemID = 3;
        expectedPantryContents.quantity = 3;
        expectedPantryContents.pantryContentID = 3;
    }

    [Test]
    public void ThenThePCPantryIDOfThePantryContentReturnedIsCorrect()
    {
        Assert.AreEqual(expectedPantryContents.pcPantryID, actualPantryContents.pcPantryID);
    }

    [Test]
    public void ThenThePCItemIDOfThePantryContentReturnedIsCorrect()
    {
        Assert.AreEqual(expectedPantryContents.pcItemID, actualPantryContents.pcItemID);
    }

    [Test]
    public void ThenTheQuantityOfThePantryContentReturnedIsCorrect()
    {
        Assert.AreEqual(expectedPantryContents.quantity, actualPantryContents.quantity);
    }

    [Test]
    public void ThenThePantryContentIDOfThePantryContentReturnedIsCorrect()
    {
        Assert.AreEqual(expectedPantryContents.pantryContentID, actualPantryContents.pantryContentID);
    }

}

public class WhenPantryContentsIsQueriedForAllPantryContents : PantryContentsTests
{
    private List<PantryContents> expectedPantryContentsList = new List<PantryContents>();
    private String expectedPantryContentsString;
    private String actualPantryContentsString;

    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();
        PantryContentsInsert();

        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/PantryContents");
            httpResponse.Wait();
            var result = httpResponse.Result.Content.ReadAsStringAsync();
            result.Wait();
            actualPantryContentsString = result.Result;
        }
        catch (Exception e)
        {
            throw;
        }

        CreateExpected();
    }

    public void CreateExpected()
    {
        PantryContents testPantryContent1 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 1,
            quantity = 3,
            pantryContentID = 1,

        };

        expectedPantryContentsList.Add(testPantryContent1);

        PantryContents testPantryContent2 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 2,
            quantity = 3,
            pantryContentID = 2,

        };

        expectedPantryContentsList.Add(testPantryContent2);

        PantryContents testPantryContent3 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 3,
            quantity = 3,
            pantryContentID = 3,
        };

        expectedPantryContentsList.Add(testPantryContent3);

        PantryContents testPantryContent4 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 2,
            quantity = 3,
            pantryContentID = 4,

        };

        expectedPantryContentsList.Add(testPantryContent4);


        PantryContents testPantryContent5 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 3,
            quantity = 3,
            pantryContentID = 5,

        };

        expectedPantryContentsList.Add(testPantryContent5);

        expectedPantryContentsString = JsonSerializer.Serialize(expectedPantryContentsList);

    }

    [Test]
    public void ThenAllPantryContentsAreReturned()
    {
        Assert.AreEqual(expectedPantryContentsString, actualPantryContentsString);
    }
}

public class WhenPantryContentIsSearchedForWithPantryContentIDAndDeleted : PantryContentsTests
{
    private List<PantryContents> expectedPantryContentsList = new List<PantryContents>();
    private String expectedPantryContentsString;
    private String actualPantryContentsString;

    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();
        PantryContentsInsert();

        try
        {
            using Task<HttpResponseMessage> httpResponse = client.PutAsync("http://localhost:8000/api/PantryContents/deletePantryContent/2", null);
            httpResponse.Wait();
        }
        catch (Exception e)
        {
            throw;
        }
        Task.Delay(1000).Wait();
        GetActual();
        Task.Delay(1000).Wait();
        CreateExpected();

    }

    public void CreateExpected()
    {
        PantryContents testPantryContent1 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 1,
            quantity = 3,
            pantryContentID = 1,

        };

        expectedPantryContentsList.Add(testPantryContent1);

        PantryContents testPantryContent3 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 3,
            quantity = 3,
            pantryContentID = 3,
        };

        expectedPantryContentsList.Add(testPantryContent3);

        PantryContents testPantryContent4 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 2,
            quantity = 3,
            pantryContentID = 4,

        };

        expectedPantryContentsList.Add(testPantryContent4);


        PantryContents testPantryContent5 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 3,
            quantity = 3,
            pantryContentID = 5,

        };

        expectedPantryContentsList.Add(testPantryContent5);

        expectedPantryContentsString = JsonSerializer.Serialize(expectedPantryContentsList);

    }

    public void GetActual()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PCPantryID, PCItemID, Quantity, PantryContentID FROM PantryContents";
            var actual = connection.Query<PantryContents>(sql);
            Task.Delay(1000).Wait();
            actualPantryContentsString = JsonSerializer.Serialize(actual);
        }
    }

    [Test]
    public void ThenThePantryContentHasBeenDeleted()
    {
        Assert.AreEqual(expectedPantryContentsString, actualPantryContentsString);
    }
}

public class WhenPantryContentIsUpdated : PantryContentsTests
{
    private PantryContents updatedPantryContent = new PantryContents();
    private PantryContents expectedUpdatedPantryContents = new PantryContents();
    private PantryContents actualUpdatedPantryContents = new PantryContents();


    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();

        ItemInsert();
        PantryInsert();
        PantryContentsInsert();
        
        CreateUpdatedPantryContent();

        try
        {
            var content = new StringContent(JsonSerializer.Serialize(updatedPantryContent), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PutAsync("http://localhost:8000/api/PantryContents", content);
            httpResponse.Wait();
        }
        catch (Exception e)
        {
            throw;
        }
        Task.Delay(1000).Wait();
        GetActual();
        Task.Delay(1000).Wait();
        CreateExpected();

    }

    public void CreateExpected()
    {
        expectedUpdatedPantryContents.pcPantryID = 3;
        expectedUpdatedPantryContents.pcItemID = 1;
        expectedUpdatedPantryContents.quantity = 54;
        expectedUpdatedPantryContents.pantryContentID = 3;
    }

    public void GetActual()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PCPantryID, PCItemID, Quantity, PantryContentID FROM PantryContents WHERE PantryContentID = 3";
            var actual = connection.Query<PantryContents>(sql);
            var result = actual.Single();
            Task.Delay(1000).Wait();
            actualUpdatedPantryContents = result;
        }
    }

    public void CreateUpdatedPantryContent()
    {
        updatedPantryContent.pcPantryID = 3;
        updatedPantryContent.pcItemID = 1;
        updatedPantryContent.quantity = 54;
        updatedPantryContent.pantryContentID = 3;
    }

    [Test]
    public void ThenThePCPantryIDIsUpdated()
    {
        Assert.AreEqual(expectedUpdatedPantryContents.pcPantryID, actualUpdatedPantryContents.pcPantryID);
    }

    [Test]
    public void ThenThePCItemIDIsUpdated()
    {
        Assert.AreEqual(expectedUpdatedPantryContents.pcItemID, actualUpdatedPantryContents.pcItemID);
    }
    
    [Test]
    public void ThenTheQuanittyIsUpdated()
    {
        Assert.AreEqual(expectedUpdatedPantryContents.quantity, actualUpdatedPantryContents.quantity);
    }
    
    [Test]
    public void ThenThePantryContentIDStayedTheSame()
    {
        Assert.AreEqual(expectedUpdatedPantryContents.pantryContentID, actualUpdatedPantryContents.pantryContentID);
    }
}
