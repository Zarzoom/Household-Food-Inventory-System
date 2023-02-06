using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Services;
namespace InventoryInCSharpAPI.Managers;

    public class PantryManager
    {
    private InventoryRepository IR {get; set;}
    public PantryManager(InventoryRepository IR)
    {
        this.IR = IR;
    }

    public Pantry addToPantyList(Pantry newPantry)
    {
        IR.addToPantryList(newPantry);
        IR.SaveChanges();
        return newPantry;
    }

    public IEnumerable<Pantry> getPantryList()
    {
        return IR.GetPantryList();
    }

    public Pantry FindPantryByPrimaryKey(long primaryKey)
    {
        return IR.FindPantryByPrimaryKey(primaryKey);
    }

    public IEnumerable<Pantry> Search(String findValue)
    {
        return IR.ContainsSearchForPantryName(findValue);
    }
    public Pantry pantryUpdate(Pantry updatedPantry)
    {
        Pantry updateMe = IR.FindPantryByPrimaryKey(updatedPantry.pantryID);
        if (updateMe != null) { 
        updateMe.pantryName = updatedPantry.pantryName;
            IR.SaveChanges();
            return updateMe;
        }
        else { return null; }
    }
    }

