namespace InventoryInCSharp.Models
{
    public class Item
    {
        private long itemID;
        public String brand { get; set; }
        public String price { get; set; }
        public String genericName { get; set; }
        public String size { get; set; }

        public Item() { }
        public Item(String brand, String price, String genericName, String size)
        {
            this.brand = brand;
            this.price = price;
            this.genericName = genericName;
            this.size = size;
        }
    }
}
