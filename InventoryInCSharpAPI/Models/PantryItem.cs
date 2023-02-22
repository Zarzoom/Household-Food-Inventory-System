namespace InventoryInCSharpAPI.Models;

public class PantryItem{
    public string PantryName {get; set;}
    public long Quantity {get; set;}
    public long PantryContentID;

    public PantryItem(){}
    public PantryItem(string PantryName, long Quantity, long PantryContentID)
    {
        this.PantryName = PantryName;
        this.Quantity = Quantity;
    }
}