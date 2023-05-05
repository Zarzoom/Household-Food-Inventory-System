namespace InventoryInCSharpAPI.Repositories
{
    using InventoryInCSharpAPI.Models;
    using Dapper;
    using MySqlConnector;

    public class ItemRepository
    {
        public ItemRepository()
        {
            
        }
        /// <summary>
        /// Sends SQL Query to Database to add Item and then select the ItemID of the last inserted value.
        /// </summary>
        /// <param name="newItem">a new item object for insertion to the database.</param>
        /// <returns>Returns the ItemID of the inserted Item as a task</returns>
        public async Task<int> AddToItemList(Item newItem)
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var sql = "INSERT INTO ItemList (GenericName, Brand, Price, Size) VALUES (@GenericName, @Brand, @Price, @Size); SELECT LAST_INSERT_ID()";
                var createdItem = await connection.QueryAsync<int>(sql, newItem);
                return createdItem.SingleOrDefault();
            }
        }
        
        /// <summary>
        /// Sends SQL Query to get all the items that are stored in the ItemList table. 
        /// </summary>
        /// <returns>Returns a list of all the Items in ItemList as a task</returns>
        public async Task<IEnumerable<Item>> GetItemList()
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var sql = "SELECT * FROM ItemList";
                var itemList = await connection.QueryAsync<Item>(sql);
                return itemList;
            }
        }
        /// <summary>
        /// Sends SQL Query to Database to search for Item using primary key.
        /// </summary>
        /// <param name="primaryKey">takes in an integer that represents the primary key that the function will be searching for.</param>
        /// <returns>Returns the Item that was found as a task </returns>
        public async Task<Item> FindItemByPrimaryKey(long primaryKey)
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var parameters = new { primaryKey };
                var sql = $"SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE ItemID = @primaryKey";
                var item = await connection.QueryAsync<Item>(sql, parameters);
                return item.SingleOrDefault();
            }
        }

        /// <summary>
        ///Sends SQL Query to Database to do a contains search of the Generic Name and Brand.
        /// If either the Generic Name or Brand contain the string regardless of case, the method will return the matching item or items.
        /// </summary>
        /// <param name="searchValue"> The string that will be searched for in the ItemList Table</param>
        /// <returns>A list of the items that contain the searchValue as a task. </returns>
        public async Task<IEnumerable<Item>> ContainsSearchForGenericNameAndBrand(String searchValue)
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                //var parameters = new {searchValue};
                var sql =
                    $"SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE LOWER(GenericName) LIKE LOWER('%{searchValue}%') OR LOWER(Brand) LIKE LOWER('%{searchValue}%')";
                var item = await connection.QueryAsync<Item>(sql);
                return item;
            }
        }
        /// <summary>
        ///Sends SQL Query to Database to update the current item entry with the new values assigned to the item. The item that needs the update is found using the primary key.
        /// Primary key cannot be updated. 
        /// </summary>
        /// <param name="updateMe">The Item, updateMe, has the primary key of the item that will be updated in ItemList.
        /// All of the other properties have the desired edits as their value.
        /// </param>
        /// <returns>Returns the updated Item as a task.</returns>
        public async Task<Item> ItemUpdate(Item updateMe)
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var sql =
                "UPDATE ItemList SET GenericName = @GenericName, Brand = @Brand, Price = @Price, Size = @Size WHERE ItemID = @ItemID; SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE ItemID = @ItemID ";
                var createdItem = await connection.QueryAsync<Item>(sql, updateMe);
                return createdItem.SingleOrDefault();
            }
        }
        
        /// <summary>
        /// Sends SQL Query to Database to Delete an Item with the ItemID that matches the parameter.
        /// </summary>
        /// <param name="ItemID"> A long that represents the primary key of the item that will be deleted. </param>
        public async void DeleteItem(long ItemID)
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var parameters = new { ItemID };
                var sql = $"DELETE FROM ItemList WHERE ItemID = @ItemID";
                var deletedRows = await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
