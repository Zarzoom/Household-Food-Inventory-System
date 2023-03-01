using System.Net.Http.Json;
using MySqlConnector;
using Dapper;
using System.Text;
using System.Text.Json;
using InventoryInCSharpAPI.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace InventoryInCSharp.IntegrationTests;

public class PantryTests
{
    protected static readonly HttpClient client = new HttpClient();

    public void InsertDirectlyToDatabase(Pantry newPantry)
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO PantryList (pantryName) VALUES (@pantryName)";
            var actual = connection.ExecuteAsync(sql, newPantry);
            actual.Wait();
        }
    }
    [OneTimeTearDown]
    public void DatabasePreparation()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "DELETE FROM PantryList";
            var actual = connection.ExecuteAsync(sql);
            actual.Wait();
        }

        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE PantryList AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
}

[TestFixture]
public class WhenPantryIsInserted : PantryTests
{
    private Pantry actualPantry = new Pantry();
    private Pantry expectedPantry = new Pantry();

    [OneTimeSetUp]
    public void Setup()
    {
        DatabasePreparation();
        try
        {
            Pantry pantry = new Pantry();
            pantry.pantryName = "Test Pantry 1";

            var content = new StringContent(JsonSerializer.Serialize(pantry), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/Pantry", content);
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
    private Pantry actualPantry = new Pantry();
    private Pantry expectedPantry = new Pantry();
    private static readonly HttpClient client = new HttpClient();

    [OneTimeSetUp]
    public void Setup()
    {
        DatabasePreparation();
        InsertPantriesForTesting();
        Task.Delay(1000).Wait();
        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Pantry/2");
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

    public void InsertPantriesForTesting()
    {
        Pantry testPantry1 = new Pantry();
        testPantry1.pantryName = "Name testPantry1";

        InsertDirectlyToDatabase(testPantry1);

        Pantry testPantry2 = new Pantry();
        testPantry2.pantryName = "Name testPantry2";

        InsertDirectlyToDatabase(testPantry2);

        Pantry testPantry3 = new Pantry();
        testPantry3.pantryName = "Name testPantry3";

        InsertDirectlyToDatabase(testPantry3);
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
