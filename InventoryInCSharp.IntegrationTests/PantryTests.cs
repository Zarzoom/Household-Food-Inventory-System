using Dapper;
using InventoryInCSharpAPI.Models;
using MySqlConnector;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
namespace InventoryInCSharp.IntegrationTests;

public class PantryTests
{
    protected static readonly HttpClient client = new HttpClient();
}

[TestFixture]
public class WhenPantryIsInserted : PantryTests
{

    [OneTimeSetUp]
    public void Setup()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        try
        {
            var pantry = new Pantry();
            pantry.pantryName = "Test Pantry 1";

            var content = new StringContent(JsonSerializer.Serialize(pantry), Encoding.UTF8, "application/json");
            using var httpResponse = client.PostAsync("http://localhost:8000/api/Pantry", content);
            httpResponse.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(1000).Wait();
        actualPantry = getActual();
        expectedPantry = getExpected();
    }
    private Pantry actualPantry = new Pantry();
    private Pantry expectedPantry = new Pantry();

    public Pantry getActual()
    {
        using (var connection = new MySqlConnection("server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT pantryName FROM PantryList";
            var actual = connection.Query<Pantry>(sql);
            return actual.Single();

        }
    }

    public Pantry getExpected()
    {
        expectedPantry.pantryName = "Test Pantry 1";
        return expectedPantry;
    }

    [Test]
    public void ThenPantryNameIsCorrect()
    {
        Assert.AreEqual(expectedPantry.pantryName, actualPantry.pantryName);
    }
}

[TestFixture]
public class WhenPantryIsInsertedDirectlyToDatabaseAndSearchedForByPrimaryKey : PantryTests
{

    [OneTimeSetUp]
    public void Setup()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();

        InsertPantriesForTesting();
        Task.Delay(1000).Wait();
        try
        {
            using var httpResponse = client.GetAsync("http://localhost:8000/api/Pantry/2");
            httpResponse.Wait();
            var result = httpResponse.Result.Content.ReadFromJsonAsync<Pantry>();
            result.Wait();
            actualPantry = result.Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(1000).Wait();
        GetExpected();
    }
    private Pantry actualPantry = new Pantry();
    private readonly Pantry expectedPantry = new Pantry();
    private static readonly HttpClient client = new HttpClient();

    public void InsertPantriesForTesting()
    {
        var testPantry1 = new Pantry();
        testPantry1.pantryName = "Name testPantry1";

        InsertMethods.PantryInsertDirectlyToDatabase(testPantry1);

        var testPantry2 = new Pantry();
        testPantry2.pantryName = "Name testPantry2";

        InsertMethods.PantryInsertDirectlyToDatabase(testPantry2);

        var testPantry3 = new Pantry();
        testPantry3.pantryName = "Name testPantry3";

        InsertMethods.PantryInsertDirectlyToDatabase(testPantry3);
    }
    public void GetExpected()
    {
        expectedPantry.pantryName = "Name testPantry2";
        expectedPantry.pantryID = 2;
    }

    [Test]
    public void ThenTheCorrectPantryNameIsFound()
    {
        Assert.AreEqual(expectedPantry.pantryName, actualPantry.pantryName);
    }

    [Test]
    public void ThenTheCorrectPrimaryKeyIsFound()
    {
        Assert.AreEqual(expectedPantry.pantryID, actualPantry.pantryID);
    }
}

[TestFixture]
public class WhenPantriesAreInsertedDirectlyToTheDatabaseAndSearchedForWithPantryNameContentsSearch : PantryTests
{

    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        InsertPantriesForTesting();
        try
        {
            Task.Delay(1000).Wait();
            using var httpResponse = client.GetAsync("http://localhost:8000/api/Pantry/search/St");
            httpResponse.Wait();
            var results = httpResponse.Result.Content.ReadAsStringAsync();
            results.Wait();
            actualPantryString = results.Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        GetExpectedPantryString();
    }
    private static readonly HttpClient client = new HttpClient();
    private string actualPantryString;
    private string expectedPantryString;
    private readonly Pantry expectedPantry1 = new Pantry();
    private Pantry expectedPantry2 = new Pantry();
    private readonly Pantry expectedPantry3 = new Pantry();
    private readonly List<Pantry> expectedPantryList = new List<Pantry>();

    public void InsertPantriesForTesting()
    {
        var TestPantry1 = new Pantry();
        TestPantry1.pantryName = "Test Name 1";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry1);

        var TestPantry2 = new Pantry();
        TestPantry2.pantryName = "This Won't Show Up";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry2);

        var TestPantry3 = new Pantry();
        TestPantry3.pantryName = "Test Name 3";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry3);
    }

    public void GetExpectedPantryString()
    {
        expectedPantry1.pantryName = "Test Name 1";
        expectedPantry1.pantryID = 1;
        expectedPantryList.Add(expectedPantry1);

        expectedPantry3.pantryName = "Test Name 3";
        expectedPantry3.pantryID = 3;
        expectedPantryList.Add(expectedPantry3);

        expectedPantryString = JsonSerializer.Serialize(expectedPantryList);
    }

    [Test]
    public void ThenPantriesAreFound()
    {
        Assert.AreEqual(expectedPantryString, actualPantryString);
    }
}

[TestFixture]
public class WhenPantryNameIsUpdated : PantryTests
{

    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        InsertPantiesForUpdate();
        Task.Delay(1000).Wait();

        try
        {
            var updatedPantry = new Pantry();
            updatedPantry.pantryName = "Correct Pantry Name";
            updatedPantry.pantryID = 2;

            var content = new StringContent(JsonSerializer.Serialize(updatedPantry), Encoding.UTF8, "application/json");
            using var httpResponse = client.PutAsync("http://localhost:8000/api/Pantry", content);
            httpResponse.Wait();

            Task.Delay(1000).Wait();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        actualPantry = GetActual();
        Task.Delay(1000).Wait();

        expectedPantry = CreateExpected();


    }
    private Pantry actualPantry = new Pantry();
    private Pantry expectedPantry = new Pantry();

    public void InsertPantiesForUpdate()
    {
        var testPantry1 = new Pantry();
        testPantry1.pantryName = "Test Pantry 1";
        InsertMethods.PantryInsertDirectlyToDatabase(testPantry1);

        var pantryForUpdate = new Pantry();
        pantryForUpdate.pantryName = "Test Failure";
        InsertMethods.PantryInsertDirectlyToDatabase(pantryForUpdate);

        var testPantry2 = new Pantry();
        testPantry2.pantryName = "Test Pantry 2";
        InsertMethods.PantryInsertDirectlyToDatabase(pantryForUpdate);
    }

    public Pantry CreateExpected()
    {
        var expectedPantry = new Pantry();
        expectedPantry.pantryName = "Correct Pantry Name";
        expectedPantry.pantryID = 2;
        return expectedPantry;
    }

    public Pantry GetActual()
    {
        using (var connection = new MySqlConnection("server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PantryName, PantryID FROM PantryList WHERE PantryID = 2";
            var actual = connection.Query<Pantry>(sql);
            return actual.Single();
        }
    }

    [Test]
    public void ThenPantryNameIsUpdated()
    {
        Assert.AreEqual(expectedPantry.pantryName, actualPantry.pantryName);
    }

    [Test]
    public void ThenPantryIDStayedTheSame()
    {
        Assert.AreEqual(expectedPantry.pantryID, actualPantry.pantryID);
    }
}

public class WhenPantryIsDeleted : PantryTests
{
    private string actualPantryContentsString;
    private string actualPantryString;
    private string expectedPantryContentsString;
    private string expectedPantryString;
    [OneTimeSetUp]
    public void SetUp()
    {

        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        ItemInsert();
        PantryInsert();
        PantryContentsInsert();
        try
        {
            Task.Delay(1000).Wait();
            using var httpResponse = client.PutAsync("http://localhost:8000/api/Pantry/deletePantry/2", null);
            httpResponse.Wait();
            Task.Delay(1000).Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        CreateExpectedPantries();
        CreateExpectedPantryContents();
        GetActualPantryString();
        GetActualPantryContentsString();

    }

    public void PantryInsert()
    {
        var TestPantry1 = new Pantry();
        TestPantry1.pantryName = "Test Name 1";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry1);

        var TestPantry2 = new Pantry();
        TestPantry2.pantryName = "Test Name 2";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry2);

        var TestPantry3 = new Pantry();
        TestPantry3.pantryName = "Test Name 3";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry3);
    }

    public void ItemInsert()
    {
        var testItem1 = new Item();
        testItem1.genericName = "Generic Name Test 1";
        testItem1.brand = "Brand Test 1";
        testItem1.price = 1.99F;
        testItem1.size = "1 Test";
        //testItem1.itemID = 1;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem1);

        var testItem2 = new Item();
        testItem2.genericName = "Generic Name Test 2";
        testItem2.brand = "Brand Test 2";
        testItem2.price = 1.99F;
        testItem2.size = "2 Test";
        //testItem2.itemID = 2;

        InsertMethods.ItemInsertDirectlyToDatabase(testItem2);

        var testItem3 = new Item();
        testItem3.genericName = "Generic Name Test 3";
        testItem3.brand = "Brand Test 3";
        testItem3.price = 1.99F;
        testItem3.size = "3 Test";

        InsertMethods.ItemInsertDirectlyToDatabase(testItem3);
    }
    public void PantryContentsInsert()
    {
        var testPantryContent1 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 1,
            quantity = 3
        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent1);

        var testPantryContent2 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 2,
            quantity = 3
        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent2);

        var testPantryContent3 = new PantryContents
        {
            pcPantryID = 2,
            pcItemID = 3,
            quantity = 3
        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent3);

        var testPantryContent4 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 2,
            quantity = 3
        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent4);

        var testPantryContent5 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 3,
            quantity = 3
        };

        InsertMethods.PantryContentInsertDirectlyToDatabase(testPantryContent5);
    }

    public void CreateExpectedPantries()
    {
        var expectedPantryList = new List<Pantry>();

        var expectedPantry1 = new Pantry();
        expectedPantry1.pantryName = "Test Name 1";
        expectedPantry1.pantryID = 1;

        expectedPantryList.Add(expectedPantry1);

        var expectedPantry2 = new Pantry();
        expectedPantry2.pantryName = "Test Name 3";
        expectedPantry2.pantryID = 3;

        expectedPantryList.Add(expectedPantry2);

        expectedPantryString = JsonSerializer.Serialize(expectedPantryList);

    }

    public void CreateExpectedPantryContents()
    {
        var expectedPantryContentsList = new List<PantryContents>();
        var expectedPantryContent1 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 2,
            quantity = 3,
            pantryContentID = 4
        };
        expectedPantryContentsList.Add(expectedPantryContent1);

        var expectedPantryContent2 = new PantryContents
        {
            pcPantryID = 3,
            pcItemID = 3,
            quantity = 3,
            pantryContentID = 5
        };

        expectedPantryContentsList.Add(expectedPantryContent2);

        expectedPantryContentsString = JsonSerializer.Serialize(expectedPantryContentsList);

    }

    public void GetActualPantryString()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PantryName, PantryID FROM PantryList";
            var actual = connection.Query<Pantry>(sql);
            actualPantryString = JsonSerializer.Serialize(actual);
        }
    }

    public void GetActualPantryContentsString()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT Quantity, PCItemID, PCPantryID, PantryContentID FROM PantryContents";
            var actual = connection.Query<PantryContents>(sql);
            actualPantryContentsString = JsonSerializer.Serialize(actual);
        }
    }

    [Test]
    public void ThenPantryContentsAreDeletedFromPantry()
    {
        Assert.AreEqual(expectedPantryContentsString, actualPantryContentsString);
    }

    [Test]
    public void ThenPantryIsDeleted()
    {
        Assert.AreEqual(expectedPantryString, actualPantryString);
    }
}

public class WhenPantryListIsQueriedForAllPantries : PantryTests
{
    private string actualPantryString;
    private readonly List<Pantry> expectedPantryList = new List<Pantry>();
    private string expectedPantryString;


    [OneTimeSetUp]
    public void SetUp()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();


        PantryInsert();


        using var httpResponse = client.GetAsync("http://localhost:8000/api/Pantry");
        httpResponse.Wait();
        var result = httpResponse.Result.Content.ReadAsStringAsync();
        result.Wait();
        actualPantryString = result.Result;

        CreateExpected();
    }

    public void PantryInsert()
    {
        var TestPantry1 = new Pantry();
        TestPantry1.pantryName = "Test Name 1";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry1);

        var TestPantry2 = new Pantry();
        TestPantry2.pantryName = "Test Name 2";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry2);

        var TestPantry3 = new Pantry();
        TestPantry3.pantryName = "Test Name 3";

        InsertMethods.PantryInsertDirectlyToDatabase(TestPantry3);
    }

    public void CreateExpected()
    {
        var TestPantry1 = new Pantry();
        TestPantry1.pantryName = "Test Name 1";
        TestPantry1.pantryID = 1;

        expectedPantryList.Add(TestPantry1);

        var TestPantry2 = new Pantry();
        TestPantry2.pantryName = "Test Name 2";
        TestPantry2.pantryID = 2;

        expectedPantryList.Add(TestPantry2);

        var TestPantry3 = new Pantry();
        TestPantry3.pantryName = "Test Name 3";
        TestPantry3.pantryID = 3;

        expectedPantryList.Add(TestPantry3);

        expectedPantryString = JsonSerializer.Serialize(expectedPantryList);
    }

    [Test]
    public void ThenAllPantriesHaveBeenReturned()
    {
        Assert.AreEqual(expectedPantryString, actualPantryString);
    }
}
