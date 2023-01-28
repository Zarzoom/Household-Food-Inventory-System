namespace InventoryInCSharp.Models
{
    public class Item
    {
        private long itemID;
        public String brand;
        public String price;
        public String genericName;
        public String size;

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
