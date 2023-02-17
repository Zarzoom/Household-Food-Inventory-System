using System.ComponentModel.DataAnnotations;
namespace InventoryInCSharpAPI.Models
{
    public class Item
    {
        [KeyAttribute]
        public long ItemID { get; set; }
        public virtual String Brand { get; set; }
        public virtual float Price { get; set; }
        public virtual String GenericName { get; set; }
        public virtual String Size { get; set; }

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
