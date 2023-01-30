namespace InventoryInCSharp.Managers;
using InventoryInCSharp.Models;
public class ItemManager
{
    private List<Item> ItemList { get; set; } = new List<Item>();

    public void addToList(Item item)
    {
        ItemList.Add(item);
    }
}
