namespace InventoryInCSharpAPI.Models
{
    public class PantryContents
    {
        public long quantity { get; set; }
        public long pcItemID { get; set; }
        public long pcPantryID { get; set; }
        public long pantryContentID { get; set; }

        public PantryContents() {}
        public PantryContents(long quantity, long pcItemId, long pcPantryId)
        {
            this.quantity = this.quantity;
            this.pcItemID = pcItemId;
            this.pcPantryID = pcPantryId;
        }
    }
}
