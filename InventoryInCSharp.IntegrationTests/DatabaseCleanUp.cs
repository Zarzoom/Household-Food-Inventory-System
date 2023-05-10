using Dapper;
using MySqlConnector;
namespace InventoryInCSharp.IntegrationTests;

public class DatabaseCleanUp
{
    [OneTimeTearDown]
    public static void ItemListDatabasePreparation()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "DELETE FROM ItemList";
            var actual = connection.ExecuteAsync(sql);
            actual.Wait();
        }

        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE ItemList AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
    
    [OneTimeTearDown]
    public static void PantryListDatabasePreparation()
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
    
    [OneTimeTearDown]
    public static void PantryContentsDatabasePreparation()
    {
        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "DELETE FROM PantryContents";
            var actual = connection.ExecuteAsync(sql);
            actual.Wait();
        }

        using (var connection =
               new MySqlConnection(
                   "server=localhost,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "ALTER TABLE PantryContents AUTO_INCREMENT = 1";
            var actual = connection.Execute(sql);
        }
    }
}
