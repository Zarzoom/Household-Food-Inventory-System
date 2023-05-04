namespace InventoryInCSharpAPI.Repositories;

using InventoryInCSharpAPI.Models;
using Dapper;
using MySqlConnector;

public class PantryRepository
{

    /// <summary>
    /// Sends SQL Query to Database to add Pantry and then select the PantryID of the last inserted value.
    /// </summary>
    /// <param name="newPantry">a new Pantry object for insertion to the database.</param>
    /// <returns>Returns the PantryID of the inserted Pantry as a task</returns>
    public async Task<int> AddToPantryList(Pantry newPantry)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO PantryList (PantryName) VALUES (@PantryName); SELECT LAST_INSERT_ID()";
            var createdItem = await connection.QueryAsync<int>(sql, newPantry);
            return createdItem.SingleOrDefault();
        }
    }
    
    /// <summary>
    /// Sends SQL Query to get all the pantries that are stored in the PantryList table. 
    /// </summary>
    /// <returns>Returns a list of all the pantries in PantryList as a task</returns>
    public async Task<IEnumerable<Pantry>> GetPantryList()
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PantryName, PantryID FROM PantryList";
            var pantryList = await connection.QueryAsync<Pantry>(sql);
            return pantryList;
        }
    }
    
    /// <summary>
    /// Sends SQL Query to Database to search for Pantry using primary key.
    /// </summary>
    /// <param name="primaryKey">takes in an integer that represents the primary key that the function will be searching for.</param>
    /// <returns>Returns the Pantry that was found as a task </returns>
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

    /// <summary>
    ///Sends SQL Query to Database to do a contains search of PantryName.
    /// If PantryName contains the string regardless of case, the method will return the matching Pantry or Pantries.
    /// </summary>
    /// <param name="searchValue"> The string that will be searched for in the PantryList Table</param>
    /// <returns>A list of the pantries that contain the searchValue as a task. </returns>
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

    /// <summary>
    ///Sends SQL Query to Database to update the current pantry entry with the new PantryName value. The pantry that needs the update is found using the primary key.
    /// Primary key cannot be updated. 
    /// </summary>
    /// <param name="updateMe">The Pantry, updateMe, has the primary key of the pantry that will be updated in PantryList.
    /// All of the other properties have the desired edits as their value.
    /// </param>
    /// <returns>Returns the updated Pantry as a task.</returns>
    public async Task<Pantry> PantryUpdate(Pantry updateMe)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "UPDATE PantryList SET PantryName = @PantryName WHERE PantryID = @PantryID; SELECT PantryID, PantryName FROM PantryList WHERE PantryID = @PantryID";
            var createdPantry = await connection.QueryAsync<Pantry>(sql, updateMe);
            return createdPantry.SingleOrDefault();
        }
    }

    /// <summary>
    /// Sends SQL Query to Database to Delete a Pantry with the PantryID that matches the parameter.
    /// </summary>
    /// <param name="PantryID"> A long that represents the primary key of the pantry that will be deleted. </param>
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
