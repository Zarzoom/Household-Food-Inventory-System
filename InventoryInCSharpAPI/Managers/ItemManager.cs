using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class ItemManager
{
    private ItemRepository IR { get; set; }
    public ItemManager(ItemRepository IR)
    {
        this.IR = IR;
    }
// private List<Item> ItemList { get; set; } = new List<Item>();

    public Item addToItemList(Item newItem) {
        IR.AddToItemList(newItem);
        return newItem;
    }

    public IEnumerable<Item> GetItemList()
    {
        var results = IR.GetItemList();
        results.Wait();
        return (results.Result);
    }

    public Item findByPrimaryKey(long primaryKey)
    {
        var results = IR.FindItemByPrimaryKey(primaryKey);
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<Item> Search(String findValue) 
    {
        var results = IR.ContainsSearchForGenericNameAndBrand(findValue);
        results.Wait();
        return (results.Result);
    }

    public void itemUpdate(Item updatedItem)
    {
      IR.itemUpdate(updatedItem);
    }
}
