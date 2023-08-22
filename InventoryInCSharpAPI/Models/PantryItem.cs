namespace InventoryInCSharpAPI.Models;

public class PantryItem
{
    public string pantryName { get; set; }
    public long quantity { get; set; }
    public long pantryContentID { get; set; }
    public long password { get; set; }


    public PantryItem() {}
    public PantryItem(string pantryName, long quantity, long pantryContentID, long password)
    {
        this.pantryContentID = pantryContentID;
        this.pantryName = pantryName;
        this.quantity = quantity;
        this.password = password;
    }
}
