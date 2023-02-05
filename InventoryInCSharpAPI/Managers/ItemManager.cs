namespace InventoryInCSharp.Managers;
using InventoryInCSharp.Models;
using InventoryInCSharpAPI.Services;

public class ItemManager
{
    private ItemRepository IR { get; set; }
    public ItemManager(ItemRepository IR)
    {
        this.IR = IR;
    }
// private List<Item> ItemList { get; set; } = new List<Item>();

    public void addToItemList(Item newItem) {
        IR.AddToItemList(newItem);
        IR.SaveChanges();
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
}
