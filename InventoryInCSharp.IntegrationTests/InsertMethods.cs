using Amazon.Util.Internal;
using Dapper;
using InventoryInCSharpAPI.Models;
using MySqlConnector;
using System.Text;
using System.Text.Json;
namespace InventoryInCSharp.IntegrationTests;

public class InsertMethods
{
    protected static readonly HttpClient client = new HttpClient();
    public static void ItemInsertDirectlyToDatabase(Item newItem)
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO ItemList (GenericName, Brand, Price, Size, Password) VALUES (@genericName, @brand, @price, @size, @Password)";
            var actual = connection.ExecuteAsync(sql, newItem);
            actual.Wait();
        }
    }
    
    public static void PantryContentInsertDirectlyToDatabase(PantryContents newPantryContent)
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO PantryContents (Quantity, PCItemID, PCPantryID, Password) VALUES (@quantity, @pcItemID, @pcPantryID, @Password)";
            var actual = connection.ExecuteAsync(sql, newPantryContent);
            actual.Wait();
        }
    }
    
    public static void PantryInsertDirectlyToDatabase(Pantry newPantry)
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO PantryList (pantryName, Password) VALUES (@pantryName, @Password)";
            var actual = connection.ExecuteAsync(sql, newPantry);
            actual.Wait();
        }
    }
    
    public static void ItemInsertUsingAPI(Item newItem)
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

    public static void LoginInsertDirectlyToDatabase(User newUser)
    {
        using (var connection = new MySqlConnection("server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO Login (UserName) VALUES (@UserName)";
            var actual = connection.ExecuteAsync(sql, newUser);
            actual.Wait();
        }
    }
}
