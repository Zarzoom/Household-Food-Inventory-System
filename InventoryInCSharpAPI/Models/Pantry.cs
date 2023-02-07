namespace InventoryInCSharpAPI.Models
{
    public class Pantry
    {
        public String pantryName {get; set; }
        public long pantryID{get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public Pantry() { 
        this.Items = new HashSet<Item>();
        }

        public Pantry(String pantryName)
        {
            this.pantryName = pantryName;
        }
    }
}
