namespace InventoryInCSharpAPI.Models;

public class Pantry
{

    public Pantry()
    {
    }

    public Pantry(string pantryName)
    {
        this.pantryName = pantryName;
    }
    public string pantryName { get; set; }
    public long pantryID { get; set; }
}
