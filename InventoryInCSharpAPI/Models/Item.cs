namespace InventoryInCSharpAPI.Models
{
    public class Item
    {
        public long itemID { get; set; }
        public String brand { get; set; }
        public float price { get; set; }
        public String genericName { get; set; }
        public String size { get; set; }

        public Item() { }
        public Item(String brand, float price, String genericName, String size)
        {
            this.brand = brand;
            this.price = price;
            this.genericName = genericName;
            this.size = size;
        }
    }
}
