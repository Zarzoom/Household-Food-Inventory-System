using InventoryInCSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryInCSharpAPI.Services
{
    public class InventoryRepository : DbContext
    {
        public DbSet<Item> ItemList { get; set; }
        public InventoryRepository(DbContextOptions<InventoryRepository> options) : base (options)
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


    }
}
