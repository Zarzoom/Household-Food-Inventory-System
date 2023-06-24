namespace InventoryInCSharp.IntegrationTests;
using System.Net.Http.Json;
using MySqlConnector;
using Dapper;
using System.Text;
using System.Text.Json;
using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Managers;

public class LoginTests
{
    private ConnectionStringAndOtherSecrets CSOS;
    protected static readonly HttpClient client = new HttpClient();

    public void userInsertForSearch()
    {
        
    }
    
}

[TestFixture]

public class WhenUserInserted: LoginTests
{
    private User expectedUser = new User();
    private User actual;

    [OneTimeSetUp]

    public void Setup()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();

        try
        {
            var user = new User();
            user.userName = "Test1";
            
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/Login", content);
            httpResponse.Wait();
            var result = httpResponse.Result.Content.ReadFromJsonAsync<User>();
            result.Wait();
            actual = result.Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(2000).Wait();
        CreateExpectedUser();

    }

    public void CreateExpectedUser()
    {
        expectedUser.userName = "Test1";
        expectedUser.password = 1;
    }

    [Test]
    public void ThenTheUserNameInsertedCorrectly()
    {
        Assert.AreEqual(expectedUser.userName, actual.userName);
    }

    public void ThenThePasswordInsertedCorrectly()
    {
        Assert.AreEqual(expectedUser.password, actual.password);
    }
}

[TestFixture]
public class WhenUserWithDuplicateUserNameIsInserted: LoginTests
{
    private String expected;
    private String actual;

    [OneTimeSetUp]

    public void Setup()
    {

        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();
        UserInsert();

        try
        {
            var user = new User();
            user.userName = "Test1";
            
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PostAsync("http://localhost:8000/api/Login", content);
            httpResponse.Wait();
            var result = httpResponse.Result.Content.ReadAsStringAsync();
            result.Wait();
            actual = result.Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(2000).Wait();

        expected = "The user name has already been taken. Please, choose another.";

    }

    public void UserInsert()
    {
        User user1 = new User();
        user1.userName = "Test1";
        
        InsertMethods.LoginInsertDirectlyToDatabase(user1);
    }



    [Test]
    public void ThenItSuccessfullyFailed()
    {
        Assert.AreEqual(expected, actual);
    }


}

[TestFixture]

public class WhenUserIsSearchedForWithUserName
{
    
}