namespace InventoryInCSharpAPI.Repositories
{
    using InventoryInCSharpAPI.Models;
    using System.Data.Common;
    using Dapper;
    using System.Data.SqlClient;
    using MySqlConnector;

    public class PantryContentsRepository
    {
        public PantryContentsRepository()
        {}
        // I could make this easier by having them add the pantryName and ItemID and then use contents search to get pantryID for them.
        public async void addToPantry(PantryContents newPantryContent){
            using(var connection = new MySqlConnection("server=db,3306;user=root;password=Your_password123;database=InventoryData;")){
                var sql = "INSERT INTO PantryContents (PCPantryID, PCItemID, Quantity, PantryContentID) VALUES (@PCPantryID, @PCItemID, @Quantity, @PantrycontentID)";
                var createdItem = await connection.ExecuteAsync(sql, newPantryContent);
        }
    }
        public async Task<IEnumerable<PantryContents>> GetAllPantryContents(){
          using(var connection = new MySqlConnection("server=db,3306;user=root;password=Your_password123;database=InventoryData;")){
                var sql= "SELECT PCItemID, PCPantryID, Quantity, PantryContentID FROM PantryContents";
                var AllPantryContents = await connection.QueryAsync<PantryContents>(sql);
                return AllPantryContents;
            }  
        }
}
}