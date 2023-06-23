namespace InventoryInCSharp.IntegrationTests;
using System.Net.Http.Json;
using MySqlConnector;
using Dapper;
using System.Text;
using System.Text.Json;
using InventoryInCSharpAPI.Models;

public class LoginTests
{
    protected static readonly HttpClient client = new HttpClient();
    
}

[TestFixture]

public class WhenUserInserted: LoginTests
{
    private User actualUser = new User();
    private User expectedUser = new User();

    [OneTimeSetUp]

    public void Setup()
    {
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.PantryContentsDatabasePreparation();

        try
        {
            var user = new User();
            user.userName = "Test1";
            
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/Login", content);
            httpResponse.Wait();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
