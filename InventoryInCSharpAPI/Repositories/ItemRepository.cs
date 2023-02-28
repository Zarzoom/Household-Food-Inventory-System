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

        public async void AddToItemList(Item newItem)
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var sql = "INSERT INTO ItemList (GenericName, Brand, Price, Size) VALUES (@GenericName, @Brand, @Price, @Size)";
                var createdItem = await connection.ExecuteAsync(sql, newItem);
            }
        }

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

        //rename this method.
        //This method does a contains search using the generic name and/or brand
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

        public async void ItemUpdate(Item updateMe)
        {
            using (var connection =
                   new MySqlConnection(
                       "server=host.docker.internal,3306;user=root;password=Your_password123;database=InventoryData;"))
            {
                var sql =
                    "UPDATE ItemList SET GenericName = @GenericName, Brand = @Brand, Price = @Price, Size = @Size WHERE ItemID = @ItemID";
                var createdItem = await connection.ExecuteAsync(sql, updateMe);
            }
        }

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
