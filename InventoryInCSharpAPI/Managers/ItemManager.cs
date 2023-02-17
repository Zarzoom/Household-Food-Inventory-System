using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Services;
using Venflow;
namespace InventoryInCSharpAPI.Managers;

public class ItemManager
{
    private InventoryRepository IR;
    public ItemManager(InventoryRepository IR)
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
        var retreivedItems= IR.GetItemList();
        retreivedItems.Wait();
        return retreivedItems.Result;
    }

    public Item findByPrimaryKey(long primaryKey)
    {
        var retreivedItems= IR.FindItemByPrimaryKey(primaryKey);
        retreivedItems.Wait();
        return retreivedItems.Result;
    }
    // public Item findWithItem(Item theItem)
    // {
    //     var retreivedItems= IR.FindItemByPrimaryKey(theItem.ItemID);
    //     retreivedItems.Wait();
    //     return retreivedItems.Result;
    //}

    public IEnumerable<Item> Search(String findValue) 
    {
        var retreivedItems= IR.ContainsSearchForGenericNameAndBrand(findValue);
        retreivedItems.Wait();
        return retreivedItems.Result;
    }

    public async Task<Item> itemUpdate(Item updatedItem)
    {
        Item updateMe = findByPrimaryKey(updatedItem.ItemID);
        if (updateMe != null) 
        {
            updateMe.Brand = updatedItem.Brand;
            updateMe.Price = updatedItem.Price;
            updateMe.GenericName = updatedItem.GenericName;
            updateMe.Size = updatedItem.Size;
            await IR.itemUpdate(updateMe);
            return updateMe;
        }
        else { return null; }

    }
}
