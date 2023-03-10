namespace InventoryInCSharpAPI.Models;

public class PantryItem
{
    public string pantryName { get; set; }
    public long quantity { get; set; }
    public long pantryContentID { get; set; }

    public PantryItem() {}
    public PantryItem(string pantryName, long quantity, long pantryContentID)
    {
        this.pantryContentID = pantryContentID;
        this.pantryName = pantryName;
        this.quantity = quantity;
    }
}
