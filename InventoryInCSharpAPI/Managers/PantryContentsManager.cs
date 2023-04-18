using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class PantryContentsManager
{
    private readonly PantryContentsRepository _PCR;
    private readonly ItemManager _IM;
    private readonly PantryRepository _PR;
    public PantryContentsManager(PantryContentsRepository PCR, ItemManager IM, PantryRepository PR)
    {
        this._PR = PR;
        this._IM = IM;
        this._PCR = PCR;
    }

    public PantryContents AddToPantry(PantryContents newPantryContent)
    {
        var doesPantryExistTask = _PR.FindPantryByPrimaryKey(newPantryContent.pcPantryID);
        doesPantryExistTask.Wait();
        Pantry doesPantryExist = doesPantryExistTask.Result;
        Item doesItemExist = _IM.FindByPrimaryKey(newPantryContent.pcItemID);
        if (doesPantryExist is not null && doesItemExist is not null)
        {
            PantryContents isItDuplicate = FindContentsByItemIDAndPantryID(newPantryContent.pcPantryID, newPantryContent.pcItemID);
            if (isItDuplicate is null)
            {
                var results = _PCR.AddToPantry(newPantryContent);
                results.Wait();
                newPantryContent.pcItemID = results.Result;
                return (newPantryContent);
            }
            else
            {
                isItDuplicate.quantity += newPantryContent.quantity;
                PantryContentUpdate(isItDuplicate);
                return isItDuplicate;
            }
        }
        else
        {
            return null;
        }
    }

    public IEnumerable<PantryContents> GetAllPantryContents()
    {
        var results = _PCR.GetAllPantryContents();
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<PantryContents> FindContentsByPCPantryID(long pcPantryID)
    {
        var results = _PCR.FindContentsByPCPantryID(pcPantryID);
        results.Wait();
        return (results.Result);
    }

    public PantryContents FindContentsByItemIDAndPantryID(long pcPantryID, long pcItemID)
    {
        var results = _PCR.FindContentsByItemIDAndPantryID(pcPantryID, pcItemID);
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<Item> WhatIsInThatPantry(long pcPantryID)
    {
        var contentsByPantryID = _PCR.FindContentsByPCPantryID(pcPantryID);
        contentsByPantryID.Wait();
        var listOfPantryContents = contentsByPantryID.Result;
        foreach (PantryContents pantryContent in listOfPantryContents)
        {
            var pantryItemList = _IM.FindByPrimaryKey(pantryContent.pcItemID);
            pantryItemList.quantity = pantryContent.quantity;
            yield return pantryItemList;
        }
    }

    public IEnumerable<PantryItem> WhereIsThatItem(long pcItemID)
    {
        var contentsByItemID = _PCR.FindContentsByPCItemID(pcItemID);
        contentsByItemID.Wait();
        var listOfItemContents = contentsByItemID.Result;
        foreach (PantryContents pantryContent in listOfItemContents)
        {
            var findPantryItems = _PR.FindPantryByPrimaryKey(pantryContent.pcPantryID);
            findPantryItems.Wait();
            PantryItem pantryItem = new PantryItem(findPantryItems.Result.pantryName, pantryContent.quantity, pantryContent.pantryContentID);
            yield return pantryItem;
        }
    }

    public PantryContents PantryContentUpdate(PantryContents updatedPantryContent)
    {
       var results = _PCR.PantryContentUpdate(updatedPantryContent);
       results.Wait();
       return (results.Result);
    }

    public void DeletePantryContent(long pantryContentID)
    {
        _PCR.DeletePantryContent(pantryContentID);
    }

    public void DeleteContentsByPantry(long pantryID)
    {
        _PCR.DeletePantryContentsByPantry(pantryID);
    }
}
