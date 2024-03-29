﻿using InventoryInCSharpAPI.Services;
namespace InventoryInCSharpAPI.Repositories
{
    using InventoryInCSharpAPI.Models;
    using Dapper;
    using MySqlConnector;

    public class ItemRepository
    {
        private ConnectionStringAndOtherSecrets CSOS;
        public ItemRepository(ConnectionStringAndOtherSecrets csos)
        {
        this.CSOS = csos;

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
                    CSOS.connection
                    ))
            {
                var sql = "INSERT INTO ItemList (GenericName, Brand, Price, Size, Password) VALUES (@GenericName, @Brand, @Price, @Size, @Password); SELECT LAST_INSERT_ID()";
                var createdItem = await connection.QueryAsyncWithRetry<int>(sql, newItem);
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
                       CSOS.connection
                   ))
            {
                var sql = "SELECT * FROM ItemList";
                var itemList = await connection.QueryAsyncWithRetry<Item>(sql);
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
                       CSOS.connection
                   ))
            {
                var parameters = new { primaryKey };
                var sql = $"SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE ItemID = @primaryKey";
                var item = await connection.QueryAsyncWithRetry<Item>(sql, parameters);
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
                       CSOS.connection
                   ))
            {
                //var parameters = new {searchValue};
                var sql =
                    $"SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE LOWER(GenericName) LIKE LOWER('%{searchValue}%') OR LOWER(Brand) LIKE LOWER('%{searchValue}%')";
                var item = await connection.QueryAsyncWithRetry<Item>(sql);
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
                       CSOS.connection
                   ))
            {
                var sql =
                "UPDATE ItemList SET GenericName = @GenericName, Brand = @Brand, Price = @Price, Size = @Size WHERE ItemID = @ItemID; SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE ItemID = @ItemID ";
                var createdItem = await connection.QueryAsyncWithRetry<Item>(sql, updateMe);
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
                       CSOS.connection
                   ))
            {
                var parameters = new { ItemID };
                var sql = $"DELETE FROM ItemList WHERE ItemID = @ItemID";
                var deletedRows = await connection.ExecuteAsyncWithRetry(sql, parameters);
            }
        }
        
        /// <summary>
        ///Sends SQL Query to Database to search for all items with a password that matches the parameter password.  
        /// </summary>
        /// <param name="password"> password is a long representing the users password that is a foreign key on the items that they have created. 
        /// </param>
        /// <returns>Returns a list of the items with a foreign key password that matches the param password.</returns>
        public async Task<IEnumerable<Item>> GetUserItems(long password)
        {
            using (var connection = new MySqlConnection(CSOS.connection))
            {
                var sql = $"SELECT ItemID, GenericName, Brand, Price, Size FROM ItemList WHERE Password LIKE '%{password}%'";
                var UserItems = await connection.QueryAsyncWithRetry<Item>(sql);
                return UserItems;
            }
        }
    }
}
