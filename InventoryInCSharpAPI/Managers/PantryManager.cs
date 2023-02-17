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
        var retreivedItems= IR.addToPantryList(newPantry);
        retreivedItems.Wait();
        return retreivedItems.Result;
    }

    public IEnumerable<Pantry> getPantryList()
    {
        var retreivedItems= IR.GetPantryList();
        retreivedItems.Wait();
        return retreivedItems.Result;
    }

    public Pantry FindPantryByPrimaryKey(long primaryKey)
    {
        var retreivedItems= IR.FindPantryByPrimaryKey(primaryKey);
        retreivedItems.Wait();
        return retreivedItems.Result;
    }

    public IEnumerable<Pantry> Search(String findValue)
    {
        var retreivedItems= IR.ContainsSearchForPantryName(findValue);
        retreivedItems.Wait();
        return retreivedItems.Result;
    }
    public async Task<Pantry> pantryUpdate(Pantry updatedPantry)
    {
        Pantry updateMe = FindPantryByPrimaryKey(updatedPantry.PantryID);
        if (updateMe != null) { 
            updateMe.PantryName = updatedPantry.PantryName;
            await IR.pantryUpdate(updateMe);
            return updateMe;
        }
        else { return null; }
    }
    }

