using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

    public class PantryManager
    {
    private PantryRepository PR {get; set;}
    private PantryContentsManager PCM { get; set; }
    public PantryManager(PantryRepository PR, PantryContentsManager PCM)
    {
        this.PCM = PCM;
        this.PR = PR;
    }

    public Pantry addToPantryList(Pantry newPantry)
    {
        PR.AddToPantryList(newPantry);
        return newPantry;
    }

    public IEnumerable<Pantry> getPantryList()
    {
        var results = PR.GetPantryList();
        results.Wait();
        return (results.Result);
    }

    public Pantry FindPantryByPrimaryKey(long primaryKey)
    {
        var results = PR.FindPantryByPrimaryKey(primaryKey);
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<Pantry> Search(String findValue)
    {
        var results = PR.ContainsSearchForPantryName(findValue);
        results.Wait();
        return (results.Result);
    }
    public void pantryUpdate(Pantry updatedPantry)
    {
        PR.pantryUpdate(updatedPantry);
    }
    public void deletePantry(long PantryID)
    {
        IEnumerable<PantryContents> itemsInPantry = PCM.FindContentsByPCPantryID(PantryID);
        if (itemsInPantry != null)
        {
            foreach (PantryContents PantryContent in itemsInPantry)
            {
                PCM.deletePantryContent(PantryContent.PCPantryID);
            }
        }
        else
        {
        }
        PR.deletePantry(PantryID);
    }
    }

