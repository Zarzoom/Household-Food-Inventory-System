namespace InventoryInCSharpAPI.Repositories;

using InventoryInCSharpAPI.Models;
using Dapper;
using MySqlConnector;

public class PantryContentsRepository
{
    public PantryContentsRepository()
    {
        
    }
    /// <summary>
    /// Sends SQL Query to database to add a new pantryContent to PantryContents table.
    /// </summary>
    /// <param name="newPantryContent"> A new PantryContents Object for insertion to the Database.</param>
    /// <returns>Returns the PantryContentsID of the inserted PantryContents as a task</returns>
    public async Task<int> AddToPantry(PantryContents newPantryContent)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "INSERT INTO PantryContents (PCPantryID, PCItemID, Quantity) VALUES (@PCPantryID, @PCItemID, @Quantity); SELECT LAST_INSERT_ID()";
            var createdItem = await connection.QueryAsync<int>(sql, newPantryContent);
            return createdItem.SingleOrDefault();
        }
    }
    
    /// <summary>
    /// Sends SQL Query to Database to update the current PantryContents entry with the new values assigned to the item.
    /// The PantryContents that needs the update is found using the primary key. Primarily used to update the quantity.
    /// </summary>
    /// <param name="updateMe">The PantryContents, updateMe, has the primary key of the PantryContents that will be updated in PantryContents.
    /// All of the other properties have the desired edits as their value.</param>
    /// <returns>Returns the updated PantryContents as a task</returns>
    public async Task<PantryContents> PantryContentUpdate(PantryContents updateMe)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "UPDATE PantryContents SET PCPantryID = @PCPantryID, PCItemID = @PCItemID, Quantity = @Quantity WHERE PantryContentID = @PantryContentID; SELECT PantryContentID, PCPantryID, PCItemID, Quantity FROM PantryContents WHERE PantryContentID = @PantryContentID";
            var createdPantryContent = await connection.QueryAsync<PantryContents>(sql, updateMe);
            return createdPantryContent.SingleOrDefault();
        }
    }

    /// <summary>
    /// Sends a SQL query to the database to get all of the PantryContents stored in the PantryContents table.
    /// </summary>
    /// <returns>Returns a list of all the PantryContents in PantryContents table as a task.</returns>
    public async Task<IEnumerable<PantryContents>> GetAllPantryContents()
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var sql = "SELECT PCItemID, PCPantryID, Quantity, PantryContentID FROM PantryContents";
            var allPantryContents = await connection.QueryAsync<PantryContents>(sql);
            return allPantryContents;
        }
    }

    /// <summary>
    /// Sends SQL query to the Database to search for PantryContents using the primary key.
    /// </summary>
    /// <param name="PCPantryID">Takes in an integer that represents the primary key that the function will be searching for.</param>
    /// <returns>Returns the PantryContent that was found as a task.</returns>
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
    
    /// <summary>
    /// Sends SQL query to the database to Delete a PantryContent with the PantryContentsID that matches the parameter. 
    /// </summary>
    /// <param name="PantryContentID">A long that represents the primary key of the item that will be deleted. </param>
    public async void DeletePantryContent(long PantryContentID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PantryContentID };
            var sql = $"DELETE FROM PantryContents WHERE PantryContentID = @PantryContentID";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }
    
    /// <summary>
    /// Sends SQL query to the database to Deletes all PantryContents that have an ItemID that matches the parameter. 
    /// </summary>
    /// <param name="PCItemID">A long that represents the ItemID that the PantryContents we want to delete will have. </param>
    public async void DeletePantryContentsByItem(long PCItemID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PCItemID };
            var sql = $"DELETE FROM PantryContents WHERE PCItemID = @PCItemID";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }
    
    /// <summary>
    /// Sends SQL query to the database to Deletes all PantryContents that have a PantryID that matches the parameter. 
    /// </summary>
    /// <param name="PCPantryID">A long that represents the PantryID that the PantryContents we want to delete will have. </param>
    public async void DeletePantryContentsByPantry(long PCPantryID)
    {
        using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
        {
            var parameters = new { PCPantryID };
            var sql = $"DELETE FROM PantryContents WHERE PCPantryID = @PCPantryID";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }
    
    /// <summary>
    /// Sends SQL query to the database to find all PantryContents that have an ItemID that matches the parameter. 
    /// </summary>
    /// <param name="PCItemID">A long that represents the ItemID that we will use to search for the pantryContents </param>
    /// <returns>Returns a list of all of the PantryContent that were found as a task.</returns>
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
    /// <summary>
    /// Sends SQL query to the database to find all PantryContents that have an ItemID and PantryID that matches the parameter. This method is used in pantry manager by AddToPantry function to make sure that duplicate pantryContents are not added to the database. 
    /// </summary>
    /// <param name="PCItemID">A long that represents the ItemID that we will use to search for the pantryContents </param>
    /// <returns>Returns a list of all of the PantryContent that were found as a task.</returns>
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
