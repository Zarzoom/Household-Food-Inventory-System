namespace InventoryInCSharpAPI.Models
{
    public class PantryContents
    {
        public long quantity {get; set;}
        public long PCItemID {get; set;}
        public long PCPantryID {get; set;}
        public long PantryContentID {get; set;}

        public PantryContents(){}
        public PantryContents(long Quantity, long PCItemID, long PCPantryID){
            this.quantity = quantity;
            this.PCItemID = PCItemID;
            this.PCPantryID = PCPantryID;
        }
    }
}
