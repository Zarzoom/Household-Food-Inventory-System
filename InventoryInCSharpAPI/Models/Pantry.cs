namespace InventoryInCSharpAPI.Models;

public class Pantry
{
    public String pantryName { get; set; }
    public long pantryID { get; set; }

    // public Pantry()
    // {
    // }

    public Pantry(String pantryName)
    {
        this.pantryName = pantryName;
    }
}
