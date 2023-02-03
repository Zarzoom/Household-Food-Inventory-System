using InventoryInCSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryInCSharpAPI.Services
{
    public class ItemRepository : DbContext
    {
        public DbSet<Item> ItemList { get; set; }
        public ItemRepository(DbContextOptions<ItemRepository> options) : base (options)
        {
        }
        public void AddToItemList(Item newItem){
            ItemList.Add(newItem);
        }
        public IEnumerable<Item> GetItemList()
        {
            return (ItemList.ToList());
        }


        //public Item createItem(Item item)
        //    {
        //        return itemRepository.save(item);
        //    }

        //public List<Item> findAll()
        //    {
        //        List<Item> itemList = new ArrayList<>();
        //        Iterator<Item> itemIterator = itemRepository.findAll().iterator();
        //        itemIterator.forEachRemaining(itemList::add);
        //        return itemList;

        //    }

        //public void delete(Item item)
        //    {
        //        itemRepository.delete(item);
        //    }
    }
}
