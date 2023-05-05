namespace InventoryInCSharpAPI.Models
{
    public class PantryContents
    {
        public long quantity { get; set; }
        public long pcItemID { get; set; }
        public long pcPantryID { get; set; }
        public long pantryContentID { get; set; }

        // public PantryContents() {}
        public PantryContents(long quantity, long pcItemID, long pcPantryID)
        {
            this.quantity = this.quantity;
            this.pcItemID = pcItemID;
            this.pcPantryID = pcPantryID;
        }
    }
}
