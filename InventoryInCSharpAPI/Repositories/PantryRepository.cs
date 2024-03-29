﻿namespace InventoryInCSharpAPI.Repositories;

using InventoryInCSharpAPI.Models;
using Dapper;
using MySqlConnector;
using InventoryInCSharpAPI.Services;


public class PantryRepository
{
    private ConnectionStringAndOtherSecrets CSOS;

    public PantryRepository(ConnectionStringAndOtherSecrets csos)
    {
        this.CSOS = csos;

    }

    /// <summary>
    /// Sends SQL Query to Database to add Pantry and then select the PantryID of the last inserted value.
    /// </summary>
    /// <param name="newPantry">a new Pantry object for insertion to the database.</param>
    /// <returns>Returns the PantryID of the inserted Pantry as a task</returns>
    public async Task<int> AddToPantryList(Pantry newPantry)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = "INSERT INTO PantryList (PantryName, Password) VALUES (@PantryName, @Password); SELECT LAST_INSERT_ID()";
            var createdItem = await connection.QueryAsyncWithRetry<int>(sql, newPantry);
            return createdItem.SingleOrDefault();
        }
    }
    
    /// <summary>
    /// Sends SQL Query to get all the pantries that are stored in the PantryList table. 
    /// </summary>
    /// <returns>Returns a list of all the pantries in PantryList as a task</returns>
    public async Task<IEnumerable<Pantry>> GetPantryList()
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = "SELECT PantryName, PantryID FROM PantryList";
            var pantryList = await connection.QueryAsyncWithRetry<Pantry>(sql);
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
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var parameters = new { primaryKey };
            var sql = $"SELECT PantryID, PantryName FROM PantryList WHERE PantryID = @primaryKey";
            var pantry = await connection.QueryAsyncWithRetry<Pantry>(sql, parameters);
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
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            //var parameters = new {searchValue};
            var sql = $"SELECT PantryID, PantryName FROM PantryList WHERE LOWER(PantryName) LIKE LOWER('%{searchValue}%')";
            var pantry = await connection.QueryAsyncWithRetry<Pantry>(sql);
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
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = "UPDATE PantryList SET PantryName = @PantryName WHERE PantryID = @PantryID; SELECT PantryID, PantryName FROM PantryList WHERE PantryID = @PantryID";
            var createdPantry = await connection.QueryAsyncWithRetry<Pantry>(sql, updateMe);
            return createdPantry.SingleOrDefault();
        }
    }

    /// <summary>
    /// Sends SQL Query to Database to Delete a Pantry with the PantryID that matches the parameter.
    /// </summary>
    /// <param name="PantryID"> A long that represents the primary key of the pantry that will be deleted. </param>
    public async void DeletePantry(long PantryID)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var parameters = new { PantryID };
            var sql = $"DELETE FROM PantryList WHERE PantryID = @PantryID";
            var deletedRows = await connection.ExecuteAsyncWithRetry(sql, parameters);
        }
    }

    /// <summary>
    ///Sends SQL Query to Database to search for all pantries with a password that matches the parameter password.  
    /// </summary>
    /// <param name="password"> password is a long representing the users password that is a foreign key on the pantries that they have created. 
    /// </param>
    /// <returns>Returns a list of the pantries with a foreign key password that matches the param password.</returns>
    public async Task<IEnumerable<Pantry>> GetUserPantries(long password)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = $"SELECT PantryID, PantryName FROM PantryList WHERE Password LIKE '%{password}%'";
            var UserPantries = await connection.QueryAsyncWithRetry<Pantry>(sql);
            return UserPantries;
        }
    }
}
