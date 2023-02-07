using InventoryInCSharpAPI.Models;
using System.Data.Entity;
using System.Data.Common;

namespace InventoryInCSharpAPI.Services
{
    public class InventoryRepository : DbContext
    {
        public DbSet<Item> ItemList { get; set; }
        public DbSet<Pantry> PantryList { get; set; }
        public InventoryRepository(DbConnection options) : base (options, false)
        {
        }
        public void AddToItemList(Item newItem){
            ItemList.Add(newItem);
        }
        public IEnumerable<Item> GetItemList()
        {
            return (ItemList.ToList());
        }
        public Item FindItemByPrimaryKey(long primaryKey )
        {
            return (ItemList.Find(primaryKey));
        }

        //rename this method.
        //This method does a contains search using the generic name and/or brand
        public IEnumerable<Item> ContainsSearchForGenericNameAndBrand(String searchValue ) 
        {
            var items = from item in ItemList
                        where item.genericName.Contains(searchValue)
                        || item.brand.Contains(searchValue)
                        select item;
            return items;
        }

        public void addToPantryList(Pantry newPantry)
        {
            PantryList.Add(newPantry);
        }

        public IEnumerable<Pantry> GetPantryList()
        {
            return (PantryList.ToList());
        }

        public Pantry FindPantryByPrimaryKey(long primaryKey)
        {
            return (PantryList.Find(primaryKey));
        }

        public IEnumerable<Pantry> ContainsSearchForPantryName(String searchValue)
        {
            var pantries = from pantry in PantryList
                        where pantry.pantryName.Contains(searchValue)
                        select pantry;
            return pantries;
        }



    }
}
