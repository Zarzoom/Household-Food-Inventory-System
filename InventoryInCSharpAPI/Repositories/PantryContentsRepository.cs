namespace InventoryInCSharpAPI.Repositories;

using InventoryInCSharpAPI.Models;
using Dapper;
using MySqlConnector;

public class PantryContentsRepository
{
    public PantryContentsRepository()
    {
    }
    // I could make this easier by having them add the pantryName and ItemID and then use contents search to get pantryID for them.
    public async Task<int> AddToPantry(PantryContents newPantryContent)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO PantryContents (PCPantryID, PCItemID, Quantity) VALUES (@PCPantryID, @PCItemID, @Quantity); SELECT LAST_INSERT_ID()";
            var createdItem = await connection.QueryAsync<int>(sql, newPantryContent);
            return createdItem.SingleOrDefault();
        }
    }

    public async void PantryContentUpdate(PantryContents updateMe)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "UPDATE PantryContents SET PCPantryID = @PCPantryID, PCItemID = @PCItemID, Quantity = @Quantity WHERE PantryContentID = @PantryContentID";
            var createdPantryContent = await connection.ExecuteAsync(sql, updateMe);
        }
    }

    public async Task<IEnumerable<PantryContents>> GetAllPantryContents()
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PCItemID, PCPantryID, Quantity, PantryContentID FROM PantryContents";
            var allPantryContents = await connection.QueryAsync<PantryContents>(sql);
            return allPantryContents;
        }
    }

    public async Task<IEnumerable<PantryContents>> FindContentsByPCPantryID(long PCPantryID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PCPantryID };
            var sql = $"SELECT PCItemID, PCPantryID, Quantity, PantryContentID FROM PantryContents WHERE PCPantryID = @PCPantryID";
            var allPantryContentWithPCPantryID = await connection.QueryAsync<PantryContents>(sql, parameters);
            return allPantryContentWithPCPantryID;
        }
    }
    public async void DeletePantryContent(long PantryContentID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PantryContentID };
            var sql = $"DELETE FROM PantryContents WHERE PantryContentID = @PantryContentID";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }
    public async void DeletePantryContentsByPantry(long PantryID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PantryID };
            var sql = $"DELETE FROM PantryContents WHERE PantryID = @PantryID";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }
    public async Task<IEnumerable<PantryContents>> FindContentsByPCItemID(long PCItemID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PCItemID };
            var sql = $"SELECT PCItemID, PCPantryID, Quantity, PantryContentID FROM PantryContents WHERE PCItemID = @PCItemID";
            var allPantryContentWithPCItemID = await connection.QueryAsync<PantryContents>(sql, parameters);
            return allPantryContentWithPCItemID;
        }
    }

    public async Task<PantryContents> FindContentsByItemIDAndPantryID(long PCPantryID, long PCItemID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PCItemID, PCPantryID };
            var sql = $"SELECT PCItemID, PCPantryID, Quantity, PantryContentID FROM PantryContents WHERE PCItemID = @PCItemID AND PCPantryID = @PCPantryID";
            var pantryContentsThatMatch = await connection.QueryAsync<PantryContents>(sql, parameters);
            return pantryContentsThatMatch.SingleOrDefault();
        }
    }
}
