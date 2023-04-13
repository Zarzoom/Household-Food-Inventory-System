using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class PantryManager
{
    private readonly PantryRepository _PR;
    private readonly PantryContentsManager _PCM;
    public PantryManager(PantryRepository PR, PantryContentsManager PCM)
    {
        this._PCM = PCM;
        this._PR = PR;
    }

    public Pantry AddToPantryList(Pantry newPantry)
    {
        var results = _PR.AddToPantryList(newPantry);
        results.Wait();
        newPantry.pantryID = results.Result;
        return newPantry;
    }

    public IEnumerable<Pantry> GetPantryList()
    {
        var results = _PR.GetPantryList();
        results.Wait();
        return (results.Result);
    }

    public Pantry FindPantryByPrimaryKey(long primaryKey)
    {
        var results = _PR.FindPantryByPrimaryKey(primaryKey);
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<Pantry> Search(String findValue)
    {
        var results = _PR.ContainsSearchForPantryName(findValue);
        results.Wait();
        return (results.Result);
    }
    public Pantry PantryUpdate(Pantry updatedPantry)
    {
       var results = _PR.PantryUpdate(updatedPantry);
       results.Wait();
       return (results.Result);
    }
    public void DeletePantry(long pantryID)
    {
        IEnumerable<PantryContents> itemsInPantry = _PCM.FindContentsByPCPantryID(pantryID);
        if (itemsInPantry != null)
        {
            foreach (PantryContents PantryContent in itemsInPantry)
            {
                _PCM.DeletePantryContent(PantryContent.pantryContentID);
            }
        }
        else
        {
        }
        _PR.DeletePantry(pantryID);
    }

    //commented out in controller to prevent catastrophic accidents.
    public void DeleteALLPantries()
    {
        IEnumerable<PantryContents> itemsInPantry;
        IEnumerable<Pantry> AllPantries = GetPantryList();
        foreach (Pantry pantry in AllPantries)
        {
            itemsInPantry = _PCM.FindContentsByPCPantryID(pantry.pantryID);

            if (itemsInPantry != null)
            {
                foreach (PantryContents PantryContent in itemsInPantry)
                {
                    _PCM.DeletePantryContent(PantryContent.pantryContentID);

                }
            }
            else
            {

            }
            _PR.DeletePantry(pantry.pantryID);
        }
    }
}
