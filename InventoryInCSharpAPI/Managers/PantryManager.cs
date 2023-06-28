using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class PantryManager
{
    private readonly PantryContentsManager _PCM;
    private readonly PantryRepository _PR;
    public PantryManager(PantryRepository PR, PantryContentsManager PCM)
    {
        _PCM = PCM;
        _PR = PR;
    }

    /// <summary>
    ///     Takes in the Pantry that need to be added to the database and calls the addPantry method from the repository.
    ///     It then converts the response from the repository from a task<Pantry> to a Pantry.
    /// </summary>
    /// <param name="newPantry">The new Pantry Object that needs to be added to the database.</param>
    /// <returns>Returns the inserted pantry as a Pantry.</returns>
    public Pantry AddToPantryList(Pantry newPantry)
    {
        var results = _PR.AddToPantryList(newPantry);
        results.Wait();
        newPantry.pantryID = results.Result;
        return newPantry;
    }

    /// <summary>
    ///     Calls the GetPantryList method (returns all pantries stored in PantryList) from Pantry Repository and converts
    ///     results from Task<IEnumerable<Pantry>> to IEnumerable<Pantry>.
    /// </summary>
    /// <returns>Returns a list of all of the Pantries in PantryList as IEnumerable<Pantry>.</returns>
    public IEnumerable<Pantry> GetPantryList()
    {
        var results = _PR.GetPantryList();
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls the FindPantryByPrimaryKey method (returns pantry whose primary key matches the parameter.) from Pantry
    ///     Repository and converts results
    ///     from Task<IEnumerable<Pantry>> to IEnumerable<Pantry>.
    /// </summary>
    /// <param name="primaryKey">
    ///     takes in an integer that represents the primary key that will be passed to
    ///     FindPantryByPrimaryKey for the Pantry search.
    /// </param>
    /// <returns>Returns the pantry that was found with the primary key as a Pantry. </returns>
    public Pantry FindPantryByPrimaryKey(long primaryKey)
    {
        var results = _PR.FindPantryByPrimaryKey(primaryKey);
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls the ContainsSearchForPantryName (Searches for a pantry whose PantryName contains the string that was taken in
    ///     as a parameter) method from Pantry Repository.
    ///     This function will then convert the return from Task<IEnumerable<Pantry>> to IEnumerable<Pantry>
    /// </summary>
    /// <param name="findValue">A string that will be used as the search term when searching pantryContents table.</param>
    /// <returns>Returns the pantries that contain the search string as a list of Pantries.</returns>
    public IEnumerable<Pantry> Search(string findValue)
    {
        var results = _PR.ContainsSearchForPantryName(findValue);
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls the PantryUpdate (updates the pantry in the database) method from PantryRepository. Converts return from a
    ///     Task<pantry> to a Pantry>
    /// </summary>
    /// <param name="updatedPantry">
    ///     Pantry Object with the same primary key as the pantry that needs the update but with new
    ///     PantryName value
    /// </param>
    /// <returns>The Updated version of the Pantry as a Pantry</returns>
    public Pantry PantryUpdate(Pantry updatedPantry)
    {
        var results = _PR.PantryUpdate(updatedPantry);
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls the DeleteContentsByPantry(Deletes PantryContents that contain that PantryID as their PCPantryID) method
    ///     and the DeletePantry (Deletes the pantry from PantryList)method from the PantryRepository.
    ///     Calling DeleteContentsByPantry makes sure that all pantryContents that are associated with the pantry are deleted
    ///     before the pantry is deleted.
    /// </summary>
    /// <param name="pantryID">pantryID is a long that represents the primary key of the pantry that needs to be deleted.</param>
    public void DeletePantry(long pantryID)
    {
        _PCM.DeleteContentsByPantry(pantryID);
        Task.Delay(500).Wait();
        _PR.DeletePantry(pantryID);
    }

    /// <summary>
    ///     Commented out in controller to prevent catastrophic accidents. This method should delete all of the pantries from
    ///     PantryList.
    ///     There was very little manual testing of this method, and it does not have any integration testing.
    /// </summary>
    public void DeleteALLPantries()
    {
        IEnumerable<PantryContents> itemsInPantry;
        var AllPantries = GetPantryList();
        foreach (var pantry in AllPantries)
        {
            itemsInPantry = _PCM.FindContentsByPCPantryID(pantry.pantryID);

            if (itemsInPantry != null)
            {
                foreach (var PantryContent in itemsInPantry)
                {
                    _PCM.DeletePantryContent(PantryContent.pantryContentID);

                }
            }
            _PR.DeletePantry(pantry.pantryID);
        }
    }
}
