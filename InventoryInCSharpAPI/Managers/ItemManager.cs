namespace InventoryInCSharp.Managers;
using InventoryInCSharp.Models;
using InventoryInCSharpAPI.Services;

public class ItemManager
{
    private InventoryRepository IR { get; set; }
    public ItemManager(InventoryRepository IR)
    {
        this.IR = IR;
    }
// private List<Item> ItemList { get; set; } = new List<Item>();

    public Item addToItemList(Item newItem) {
        IR.AddToItemList(newItem);
        IR.SaveChanges();
        return newItem;
    }

    public IEnumerable<Item> GetItemList()
    {
        return (IR.GetItemList());
    }

    public Item findByPrimaryKey(long primaryKey)
    {
        return IR.FindItemByPrimaryKey(primaryKey);
    }

    public IEnumerable<Item> Search(String findValue) 
    {
        return IR.ContainsSearchForGenericNameAndBrand(findValue);
    }

    public Item itemUpdate(Item updatedItem)
    {
        Item updateMe = IR.FindItemByPrimaryKey(updatedItem.itemID);
        if (updateMe != null) 
        {
            updateMe.brand = updatedItem.brand;
            updateMe.price = updatedItem.price;
            updateMe.genericName = updatedItem.genericName;
            updateMe.size = updatedItem.size;
            IR.SaveChanges();
            return updateMe;
        }
        else { return null; }

    }
}
