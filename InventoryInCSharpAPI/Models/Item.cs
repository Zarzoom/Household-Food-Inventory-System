namespace InventoryInCSharpAPI.Models;

public class Item
{

    public Item()
    {
    }
    public Item(string brand, float price, string genericName, string size)
    {
        this.brand = brand;
        this.price = price;
        this.genericName = genericName;
        this.size = size;
    }
    public long itemID { get; set; }
    public string brand { get; set; }
    public float price { get; set; }
    public string genericName { get; set; }
    public string size { get; set; }
    public long quantity { get; set; }
}
