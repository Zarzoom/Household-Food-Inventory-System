using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class ItemManager
{
    private readonly ItemRepository _IR;
    public ItemManager(ItemRepository IR)
    {
        this._IR = IR;
    }
// private List<Item> ItemList { get; set; } = new List<Item>();

    public Item AddToItemList(Item newItem)
    {
        _IR.AddToItemList(newItem);
        return newItem;
    }

    public IEnumerable<Item> GetItemList()
    {
        var results = _IR.GetItemList();
        results.Wait();
        return (results.Result);
    }

    public Item FindByPrimaryKey(long primaryKey)
    {
        var results = _IR.FindItemByPrimaryKey(primaryKey);
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<Item> Search(String findValue)
    {
        var results = _IR.ContainsSearchForGenericNameAndBrand(findValue);
        results.Wait();
        return (results.Result);
    }

    public void ItemUpdate(Item updatedItem)
    {
        _IR.ItemUpdate(updatedItem);
    }

    public void DeleteItem(long itemID)
    {
        _IR.DeleteItem(itemID);
    }

    //commented out in controller to prevent catastrophic accidents.

    public void DeleteALLItems()
    {
        IEnumerable<Item> allItems = GetItemList();
        foreach (Item item in allItems)
        {
            DeleteItem(item.itemID);
        }
    }
}
