using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using InventoryInCSharpAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InventoryInCSharp.IntegrationTests.LoginTests;

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

public class WhenUserIsSearchedForWithUserName: LoginTests
{
    User actual = new User();
    User expected= new User();
    
    [OneTimeSetUp]
    public void Setup()
    {

        
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();
        InsertUserForTesting();
        Task.Delay(2000).Wait();
        try
        {
            using Task<HttpResponseMessage> httpResponse = client.GetAsync("http://localhost:8000/api/Login/usernameSearch/Test2");
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

        expected.userName = "test2";
        expected.password = 2;
    }
    public void InsertUserForTesting()
    {
        User testUser1 = new User();
        testUser1.userName = "test1";
        
        InsertMethods.LoginInsertDirectlyToDatabase(testUser1);
        
        User testUser2 = new User();
        testUser2.userName = "test2";
        
        InsertMethods.LoginInsertDirectlyToDatabase(testUser2);
        
        User testUser3 = new User();
        testUser3.userName = "test3";
        
        InsertMethods.LoginInsertDirectlyToDatabase(testUser3);
    }

    [Test]

    public void ThenCorrectPasswordIsFound()
    {
        Assert.AreEqual(expected: expected.password, actual: actual.password);
    }

    public void ThenCorrectUserNameIsFound()
    {
        Assert.AreEqual(expected: expected.userName, actual: actual.userName);
    }

}

public class WhenUserIsSearchedForWithUserNameAndPassword : LoginTests
{
    HttpStatusCode actual;
    HttpStatusCode expected = HttpStatusCode.OK;

    [OneTimeSetUp]
    public void Setup()
    {


        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();
        InsertUserForTesting();
        Task.Delay(2000).Wait();

        User forSearch = new User();
        forSearch.password = 2;
        forSearch.userName = "test2";
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(forSearch), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PutAsync("http://localhost:8000/api/Login/LoginSearch", content);
            httpResponse.Wait();
            var result = httpResponse.Result.StatusCode;
            actual = result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(2000).Wait();
        
    }
    public void InsertUserForTesting()
    {
        User testUser1 = new User();
        testUser1.userName = "test1";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser1);

        User testUser2 = new User();
        testUser2.userName = "test2";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser2);

        User testUser3 = new User();
        testUser3.userName = "test3";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser3);
    }

    [Test]

    public void ThenUserIsFound()
    {
        Assert.AreEqual(expected: expected, actual: actual);
    }
    
}

public class WhenUserIsSearchedForWithIncorrectUserNameAndCorrectPassword : LoginTests
{
    HttpStatusCode actual;
    String actualMessage;
    HttpStatusCode expected = HttpStatusCode.NotFound;
    String expectedMessage = "User name or password is incorrect.";

    [OneTimeSetUp]
    public void Setup()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();
        InsertUserForTesting();
        Task.Delay(2000).Wait();

        User forSearch = new User();
        forSearch.password = 2;
        forSearch.userName = "testing123";
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(forSearch), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PutAsync("http://localhost:8000/api/Login/LoginSearch", content);
            httpResponse.Wait();
            var messageResult = httpResponse.Result.Content.ReadAsStringAsync();
            messageResult.Wait();
            actualMessage = messageResult.Result;
            var result = httpResponse.Result.StatusCode;
            actual = result;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(2000).Wait();
        
    }
    public void InsertUserForTesting()
    {
        User testUser1 = new User();
        testUser1.userName = "test1";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser1);

        User testUser2 = new User();
        testUser2.userName = "test2";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser2);

        User testUser3 = new User();
        testUser3.userName = "test3";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser3);
    }

    [Test]

    public void ThenTheErrorStatusCodeIsReturned()
    {
        Assert.AreEqual(expected: expected, actual: actual);
    }
    public void ThenTheErrorMessageIsReturned()
    {
        Assert.AreEqual(expected: expectedMessage, actual: actualMessage);
    }
    
}

public class WhenUserIsSearchedForWithCorrectUserNameAndIncorrectPassword : LoginTests
{
    HttpStatusCode actual;
    String actualMessage;
    HttpStatusCode expected = HttpStatusCode.NotFound;
    String expectedMessage = "User name or password is incorrect.";

    [OneTimeSetUp]
    public void Setup()
    {
        DatabaseCleanUp.PantryContentsDatabasePreparation();
        DatabaseCleanUp.PantryListDatabasePreparation();
        DatabaseCleanUp.ItemListDatabasePreparation();
        DatabaseCleanUp.LoginDatabasePreparation();
        InsertUserForTesting();
        Task.Delay(2000).Wait();

        User forSearch = new User();
        forSearch.password = 3;
        forSearch.userName = "testing2";
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(forSearch), Encoding.UTF8, "application/json");
            using Task<HttpResponseMessage> httpResponse = client.PutAsync("http://localhost:8000/api/Login/LoginSearch", content);
            httpResponse.Wait();
            var messageResult = httpResponse.Result.Content.ReadAsStringAsync();
            messageResult.Wait();
            actualMessage = messageResult.Result;
            var result = httpResponse.Result.StatusCode;
            actual = result;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Task.Delay(2000).Wait();
        
    }
    public void InsertUserForTesting()
    {
        User testUser1 = new User();
        testUser1.userName = "test1";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser1);

        User testUser2 = new User();
        testUser2.userName = "test2";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser2);

        User testUser3 = new User();
        testUser3.userName = "test3";

        InsertMethods.LoginInsertDirectlyToDatabase(testUser3);
    }

    [Test]

    public void ThenTheErrorStatusCodeIsReturned()
    {
        Assert.AreEqual(expected: expected, actual: actual);
    }
    public void ThenTheErrorMessageIsReturned()
    {
        Assert.AreEqual(expected: expectedMessage, actual: actualMessage);
    }
}