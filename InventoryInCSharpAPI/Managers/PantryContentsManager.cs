using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class PantryContentsManager
{
    private readonly ItemManager _IM;
    private readonly PantryContentsRepository _PCR;
    private readonly PantryRepository _PR;
    public PantryContentsManager(PantryContentsRepository PCR, ItemManager IM, PantryRepository PR)
    {
        _PR = PR;
        _IM = IM;
        _PCR = PCR;
    }

    /// <summary>
    ///     Adds PantryContents to PantryContents table.
    ///     The method does a search for both the pantry and item that are part of the PantryContents in the parameter using
    ///     their primary key.
    ///     This ensures that the pantry and item exist and that we don't get a foreign key error.
    ///     The method then searches the PantryContents table using the pcItemID and pcPantryID. This makes sure that the
    ///     PantryContents do not already exist in the table.
    ///     If they do the function will update the quantity of the existing pantryContent and will add the new quantity to the
    ///     old.
    ///     If it doesn't exist then the function will call the add function from PantryContentsRepository.
    /// </summary>
    /// <param name="newPantryContent">A pantryContents object with the data of the desired new PantryContents</param>
    /// <returns>Returns the new pantryContent or the updated PantryContent. </returns>
    public PantryContents AddToPantry(PantryContents newPantryContent)
    {
        var doesPantryExistTask = _PR.FindPantryByPrimaryKey(newPantryContent.pcPantryID);
        doesPantryExistTask.Wait();
        var doesPantryExist = doesPantryExistTask.Result;
        var doesItemExist = _IM.FindByPrimaryKey(newPantryContent.pcItemID);
        if (doesPantryExist is not null && doesItemExist is not null)
        {
            var isItDuplicate = FindContentsByItemIDAndPantryID(newPantryContent.pcPantryID, newPantryContent.pcItemID);
            if (isItDuplicate is null)
            {
                var results = _PCR.AddToPantry(newPantryContent);
                results.Wait();
                newPantryContent.pcItemID = results.Result;
                return newPantryContent;
            }
            isItDuplicate.quantity += newPantryContent.quantity;
            PantryContentUpdate(isItDuplicate);
            return isItDuplicate;
        }
        return null;
    }

    /// <summary>
    ///     This function calls GetAllPantryContents (sends sql query to get all pantryContents from database) from
    ///     PantryContentsRepository.
    ///     Converts the return from task<IEnumerable<PantryContents>> to IEnumerable<PantryContents>.
    /// </summary>
    /// <returns>Returns a list of all the PantryContents in the PantryContents Table</returns>
    public IEnumerable<PantryContents> GetAllPantryContents()
    {
        var results = _PCR.GetAllPantryContents();
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls FindContentsByPCPantryID(sends sql query to search database using the pcPantryID of pantryContents) from
    ///     pantryContentsRepository.
    ///     Converts the return from task<IEnumerable<PantryContents>> to IEnumerable<PantryContents>.
    /// </summary>
    /// <param name="pcPantryID">
    ///     takes in an integer that represents the pcPantryID that will be passed to
    ///     FindPantryContentsByPCPantryID for the PantryContents search
    /// </param>
    /// <returns>Returns a list of all the PantryContents whose PCPantryID matches the parameter.</returns>
    public IEnumerable<PantryContents> FindContentsByPCPantryID(long pcPantryID)
    {
        var results = _PCR.FindContentsByPCPantryID(pcPantryID);
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls FindContentsByPCItemID(sends sql query to search database using the pcItemID of pantryContents) from
    ///     pantryContentsRepository.
    ///     Converts the return from task<IEnumerable<PantryContents>> to IEnumerable<PantryContents>.
    /// </summary>
    /// <param name="pcItemID">
    ///     takes in an integer that represents the pcItemID that will be passed to
    ///     FindPantryContentsByPCPantryID for the PantryContents search
    /// </param>
    /// <returns>Returns a list of all the PantryContents whose PCItemID matches the parameter.</returns>
    public IEnumerable<PantryContents> FindContentsByPCItemID(long pcItemID)
    {
        var results = _PCR.FindContentsByPCItemID(pcItemID);
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls FindContentsByPCItemIDAndPantryID(sends sql query to search database using the pcItemID and pcPantryID of
    ///     pantryContents) from pantryContentsRepository.
    ///     Converts the return from task<IEnumerable<PantryContents>> to IEnumerable<PantryContents>.
    /// </summary>
    /// <param name="pcItemID">
    ///     takes in an integer that represents the pcItemID that will be passed to
    ///     FindPantryContentsByPCPantryIDAndPCItemID for the PantryContents search
    /// </param>
    /// <param name="pcPantryID">
    ///     takes in an integer that represents the pcPantryID that will be passed to
    ///     FindPantryContentsByPCPantryIDAndPCItemID for the PantryContents search
    /// </param>
    /// <returns>Returns a list of all the PantryContents whose PCItemID and PCPantryID matches the parameters.</returns>
    public PantryContents FindContentsByItemIDAndPantryID(long pcPantryID, long pcItemID)
    {
        var results = _PCR.FindContentsByItemIDAndPantryID(pcPantryID, pcItemID);
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     This function calls FindContentsByPCPantryID(Searches for PantryContents with a matching pcPantryID) from
    ///     PantryContentsRepository.
    ///     This returns all of the pantryContents that are in that Pantry.
    ///     The function then iterates over the list of PantryContents returned and searched for the Items associated with the
    ///     pantryContents using PCItemID.
    ///     This returns a list of items that are associated with the pantry in pantryContents.
    /// </summary>
    /// <param name="pcPantryID"> This long represents the PantryID that will be searched for in PantryContents</param>
    /// <returns>A list of the Items that are associated with the pantry.</returns>
    public IEnumerable<Item> WhatIsInThatPantry(long pcPantryID)
    {
        var contentsByPantryID = _PCR.FindContentsByPCPantryID(pcPantryID);
        contentsByPantryID.Wait();
        var listOfPantryContents = contentsByPantryID.Result;
        foreach (var pantryContent in listOfPantryContents)
        {
            var pantryItemList = _IM.FindByPrimaryKey(pantryContent.pcItemID);
            pantryItemList.quantity = pantryContent.quantity;
            yield return pantryItemList;
        }
    }

    /// <summary>
    ///     Finds the pantries that the item associated with the ItemID specified in the parameter. The function searches
    ///     PantryContents using the pcITemID.
    ///     It then uses the PantryContents that are returned to find the pantries using the pcPantryID. It returns a list of
    ///     all of the panries that are associated with the item.
    /// </summary>
    /// <param name="pcItemID">This long represents the ItemID that will be searched for in PantryContents</param>
    /// <returns>A list of the pantries that are associated with the item.</returns>
    public IEnumerable<PantryItem> WhereIsThatItem(long pcItemID)
    {
        var contentsByItemID = _PCR.FindContentsByPCItemID(pcItemID);
        contentsByItemID.Wait();
        var listOfItemContents = contentsByItemID.Result;
        foreach (var pantryContent in listOfItemContents)
        {
            var findPantryItems = _PR.FindPantryByPrimaryKey(pantryContent.pcPantryID);
            findPantryItems.Wait();
            var pantryItem = new PantryItem(findPantryItems.Result.pantryName, pantryContent.quantity, pantryContent.pantryContentID);
            yield return pantryItem;
        }
    }

    /// <summary>
    ///     Calls the PantryContentsUpdate (updates the pantryContents in the database) method from PantryContentsRepository.
    ///     Converts return from a Task<pantryContents> to a PantryContents.
    /// </summary>
    /// <param name="updatedPantryContent">
    ///     PantryContents Object with the same primary key as the pantryContents that needs
    ///     the update but with new values for the other properties
    /// </param>
    /// <returns>The Updated version of the PantryContents as a PantryContents</returns>
    public PantryContents PantryContentUpdate(PantryContents updatedPantryContent)
    {
        var results = _PCR.PantryContentUpdate(updatedPantryContent);
        results.Wait();
        return results.Result;
    }

    /// <summary>
    ///     Calls the DeletePantryContent function from the PantryContentsRepository so that the PantryContent with the same ID
    ///     as the parameter gets deleted.
    /// </summary>
    /// <param name="pantryContentID">This long represents the ItemID that will be searched for in PantryContents</param>
    public void DeletePantryContent(long pantryContentID)
    {
        _PCR.DeletePantryContent(pantryContentID);
    }
    /// <summary>
    ///     Calls the DeletePantryContentsByItem function from the PantryContentsRepository. This will delete all Pantry
    ///     contents that have a pcItemID that matches the parameter.
    /// </summary>
    /// <param name="ItemID">This long represents the ItemID that will be searched for in PantryContents</param>
    public void DeleteContentsByItem(long itemID)
    {
        _PCR.DeletePantryContentsByItem(itemID);
    }

    /// <summary>
    ///     Calls the DeletePantryContentsByPantry function from the PantryContentsRepository. This will delete all Pantry
    ///     contents that have a pcPantryID that matches the parameter.
    /// </summary>
    /// <param name="PantryID">This long represents the PantryID that will be searched for in PantryContents</param>
    public void DeleteContentsByPantry(long pantryID)
    {
        _PCR.DeletePantryContentsByPantry(pantryID);
    }
}
