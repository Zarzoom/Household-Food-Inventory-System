
namespace InventoryInCSharpAPI.Repositories
{
using InventoryInCSharpAPI.Models;
using System.Data.Common;
using Dapper;
    using System.Data.SqlClient;
    using MySqlConnector;

    public class PantryRepository
    {
         public PantryRepository()
        {
        }

        public async void AddToPantryList(Pantry newPantry){
            using(var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;")){
                var sql = "INSERT INTO PantryList (PantryName) VALUES (@PantryName)";
                var createdItem = await connection.ExecuteAsync(sql, newPantry);
        }
            }
        public async Task<IEnumerable<Pantry>> GetPantryList()
        {
            using(var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;")){
                var sql= "SELECT PantryName, PantryID FROM PantryList";
                var PantryList = await connection.QueryAsync<Pantry>(sql);
                return PantryList;
            }
        }
        public async Task<Pantry> FindPantryByPrimaryKey(long primaryKey )
        {
             using(var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;")){
                var parameters = new {primaryKey};
                var sql= $"SELECT PantryID, PantryName FROM PantryList WHERE PantryID = @primaryKey";
                var Pantry = await connection.QueryAsync<Pantry>(sql, parameters);
                return Pantry.Single();
            }
        }

        //rename this method.
        //This method does a contains search using the generic name and/or brand
        public async Task<IEnumerable<Pantry>> ContainsSearchForPantryName(String searchValue ) 
        {
            using(var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;")){
                //var parameters = new {searchValue};
                var sql= $"SELECT PantryID, PantryName FROM PantryList WHERE LOWER(PantryName) LIKE LOWER('%{searchValue}%')";
                var Pantry = await connection.QueryAsync<Pantry>(sql);
                return Pantry;
            }

        }

        public async void pantryUpdate(Pantry updateMe){
            using(var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;")){
            var sql = "UPDATE PantryList SET PantryName = @PantryName WHERE PantryID = @PantryID";
            var createdPantry = await connection.ExecuteAsync(sql, updateMe);
        }
        }
        
        public async void deletePantry(long PantryID){
            using (var connection = new MySqlConnection("server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var parameters = new { PantryID};
                var sql = $"DELETE FROM PantryList WHERE PantryID = @PantryID";
                var deletedRows = await connection.ExecuteAsync(sql, parameters);
            } 
        }

    }
}

