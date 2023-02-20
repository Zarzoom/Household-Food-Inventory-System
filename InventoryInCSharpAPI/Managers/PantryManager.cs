using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

    public class PantryManager
    {
    private PantryRepository IR {get; set;}
    public PantryManager(PantryRepository IR)
    {
        this.IR = IR;
    }

    public Pantry addToPantryList(Pantry newPantry)
    {
        IR.AddToPantryList(newPantry);
        return newPantry;
    }

    public IEnumerable<Pantry> getPantryList()
    {
        var results = IR.GetPantryList();
        results.Wait();
        return (results.Result);
    }

    public Pantry FindPantryByPrimaryKey(long primaryKey)
    {
        var results = IR.FindPantryByPrimaryKey(primaryKey);
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<Pantry> Search(String findValue)
    {
        var results = IR.ContainsSearchForPantryName(findValue);
        results.Wait();
        return (results.Result);
    }
    public void pantryUpdate(Pantry updatedPantry)
    {
        IR.pantryUpdate(updatedPantry);
    }
    }

