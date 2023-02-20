namespace InventoryInCSharpAPI.Repositories
{
using InventoryInCSharpAPI.Models;
using System.Data.Common;
using Dapper;
    using System.Data.SqlClient;
    using MySqlConnector;

    public class ItemRepository
    {
         public ItemRepository()
        {
        }

        public async void AddToItemList(Item newItem){
            using(var connection = new MySqlConnection("server=db,3306;user=root;password=Your_password123;database=InventoryData;")){
                var sql = "INSERT INTO ItemList (GenericName, Brand, Price, Size) VALUES (@GenericName, @Brand, @Price, @Size)";
                var createdItem = await connection.ExecuteAsync(sql, newItem);
        }
            }
        public async Task<IEnumerable<Item>> GetItemList()
        {
            using(var connection = new MySqlConnection("server=db,3306;user=root;password=Your_password123;database=InventoryData;")){
                var sql= "SELECT * FROM ItemList";
                var ItemList = await connection.QueryAsync<Item>(sql);
                return ItemList;
            }
        }
        public async Task<Item> FindItemByPrimaryKey(long primaryKey )
        {
             using(var connection = new MySqlConnection("server=db,3306;user=root;password=Your_password123;database=InventoryData;")){
                var parameters = new {primaryKey};
                var sql= $"SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE ItemID = @primaryKey";
                var Item = await connection.QueryAsync<Item>(sql, parameters);
                return Item.Single();
            }
        }

        //rename this method.
        //This method does a contains search using the generic name and/or brand
        public async Task<IEnumerable<Item>> ContainsSearchForGenericNameAndBrand(String searchValue ) 
        {
            using(var connection = new MySqlConnection("server=db,3306;user=root;password=Your_password123;database=InventoryData;")){
                //var parameters = new {searchValue};
                var sql= $"SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE LOWER(GenericName) LIKE LOWER('%{searchValue}%') OR LOWER(Brand) LIKE LOWER('%{searchValue}%')";
                var Item = await connection.QueryAsync<Item>(sql);
                return Item;
            }

        }

        public async void itemUpdate(Item updateMe){
            using(var connection = new MySqlConnection("server=db,3306;user=root;password=Your_password123;database=InventoryData;")){
            var sql = "UPDATE ItemList SET GenericName = @GenericName, Brand = @Brand, Price = @Price, Size = @Size WHERE ItemID = @ItemID";
            var createdItem = await connection.ExecuteAsync(sql, updateMe);
        }
        }

    }
}
