using InventoryInCSharpAPI.Models;
using Venflow;

namespace InventoryInCSharpAPI.Services
{
    public class InventoryRepository : Database
    {
        public Table<Item> ItemList { get; set; }
        public Table<Pantry> PantryList { get; set; }
        public InventoryRepository(DatabaseOptionsBuilder DBO) : base (DBO)
        {
        }
        // public async void additemrepositorytoList(InventoryRepository h){
        //     await ItemList.Insert
        // }
        public async void AddToItemList(Item newItem){
            await ItemList.Insert().InsertAsync(newItem);
            //ItemList.Add(newItem);
        }
        public async Task<List<Item>> GetItemList()
        {
            return await ItemList.QueryBatch($"SELECT \"ItemID\", \"GenericName\", \"Brand\", \"Size\", \"Price\" FROM \"ItemList\"").QueryAsync();
        }
        public async Task <Item> FindItemByPrimaryKey(long primaryKey )
        {
            return await ItemList.QuerySingle( $"SELECT \"ItemID\",\"Brand\",\"GenericName\",\"Price\",\"Size\" FROM \"ItemList\" WHERE \"ItemID\"= {primaryKey}").TrackChanges().QueryAsync();
        }

        //rename this method.
        //This method does a contains search using the generic name and/or brand
        //This method needs a thing to block sql injection.
        public async Task<List<Item>> ContainsSearchForGenericNameAndBrand(String searchValue ) 
        {
             return await ItemList.QueryBatch($"SELECT \"ItemID\",\"Brand\",\"GenericName\",\"Price\",\"Size\" FROM \"ItemList\" WHERE LOWER(\"Brand\") like LOWER('%{searchValue}%') OR LOWER(\"GenericName\") like LOWER('%{searchValue}%')").QueryAsync();

             //return await ItemList.QueryBatch($"SELECT \"ItemID\",\"Brand\",\"GenericName\",\"Price\",\"Size\" FROM \"ItemList\" WHERE \"Brand\" like '%{searchValue}%' OR \"GenericName\" like '%{searchValue}%'").QueryAsync();
            // var items = from item in ItemList
            //             where item.genericName.Contains(searchValue)
            //             || item.brand.Contains(searchValue)
            //             select item;
            // return items;
        }

        public async Task<Pantry> addToPantryList(Pantry newPantry)
        {
            await PantryList.Insert().InsertAsync(newPantry);
            return newPantry;
        }

        public async Task<List<Pantry>> GetPantryList()
        {
            return await PantryList.QueryBatch($"SELECT \"PantryID\", \"PantryName\" FROM \"PantryList\"").QueryAsync();
        }

        public async Task <Pantry> FindPantryByPrimaryKey(long primaryKey)
        {
                return await PantryList.QuerySingle( $"SELECT \"PantryID\",\"PantryName\" FROM \"PantryList\" WHERE \"PantryID\"= \"{primaryKey}\"").QueryAsync();

        }

        public async Task <List<Pantry>> ContainsSearchForPantryName(String searchValue)
        {
             return await PantryList.QueryBatch($"SELECT \"PantryID\", \"PantryName\" FROM \"PantryList\" WHERE \"PantryID\" like \"%{searchValue}%\" OR \"PantryName\" like \"%{searchValue}%\"").QueryAsync();
        }

        public async Task <Item> itemUpdate(Item updateMe){
            
            //ItemList.TrackChanges(ref updateMe);
            await ItemList.UpdateAsync(updateMe);
            return updateMe;
        }

        public async Task<Pantry> pantryUpdate(Pantry updateMe){
            await PantryList.UpdateAsync(updateMe);
            return updateMe;
        }



    }
}
