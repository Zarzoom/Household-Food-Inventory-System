using System.ComponentModel.DataAnnotations;
namespace InventoryInCSharpAPI.Models
{
    public class Pantry
    {
        public String PantryName {get; set; }
        
        [KeyAttribute]
        public long PantryID{get; set; }

        public Pantry()
        {
            
        }
        public Pantry(String pantryName)
        {
            this.PantryName = pantryName;
        }
    }
}
