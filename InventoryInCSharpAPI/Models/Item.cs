using System.ComponentModel.DataAnnotations;
namespace InventoryInCSharpAPI.Models
{
    public class Item
    {
        [KeyAttribute]
        public long ItemID { get; set; }
        public String Brand { get; set; }
        public float Price { get; set; }
        public String GenericName { get; set; }
        public String Size { get; set; }

        public Item() { }
        public Item(String brand, float price, String genericName, String size)
        {
            this.Brand = brand;
            this.Price = price;
            this.GenericName = genericName;
            this.Size = size;
        }
    }
}
