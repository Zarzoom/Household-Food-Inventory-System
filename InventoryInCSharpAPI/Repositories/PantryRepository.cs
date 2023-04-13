namespace InventoryInCSharpAPI.Repositories;

using InventoryInCSharpAPI.Models;
using Dapper;
using MySqlConnector;

public class PantryRepository
{
    public PantryRepository()
    {
    }

    public async Task<int> AddToPantryList(Pantry newPantry)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO PantryList (PantryName) VALUES (@PantryName); SELECT LAST_INSERT_ID()";
            var createdItem = await connection.QueryAsync<int>(sql, newPantry);
            return createdItem.SingleOrDefault();
        }
    }
    public async Task<IEnumerable<Pantry>> GetPantryList()
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PantryName, PantryID FROM PantryList";
            var pantryList = await connection.QueryAsync<Pantry>(sql);
            return pantryList;
        }
    }
    public async Task<Pantry> FindPantryByPrimaryKey(long primaryKey)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { primaryKey };
            var sql = $"SELECT PantryID, PantryName FROM PantryList WHERE PantryID = @primaryKey";
            var pantry = await connection.QueryAsync<Pantry>(sql, parameters);
            return pantry.SingleOrDefault();
        }
    }

    //rename this method.
    //This method does a contains search using the generic name and/or brand
    public async Task<IEnumerable<Pantry>> ContainsSearchForPantryName(String searchValue)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            //var parameters = new {searchValue};
            var sql = $"SELECT PantryID, PantryName FROM PantryList WHERE LOWER(PantryName) LIKE LOWER('%{searchValue}%')";
            var pantry = await connection.QueryAsync<Pantry>(sql);
            return pantry;
        }

    }

    public async Task<Pantry> PantryUpdate(Pantry updateMe)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "UPDATE PantryList SET PantryName = @PantryName WHERE PantryID = @PantryID; SELECT PantryID, PantryName FROM PantryList WHERE PantryID = @PantryID";
            var createdPantry = await connection.QueryAsync<Pantry>(sql, updateMe);
            return createdPantry.SingleOrDefault();
        }
    }

    public async void DeletePantry(long PantryID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PantryID };
            var sql = $"DELETE FROM PantryList WHERE PantryID = @PantryID";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }

}
