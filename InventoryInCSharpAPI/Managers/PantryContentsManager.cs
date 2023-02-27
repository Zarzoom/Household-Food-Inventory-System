using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class PantryContentsManager
{
    private PantryContentsRepository PCR { get; set; }
    private ItemRepository IR { get; set; }
    private PantryRepository PR {get; set;}
    public PantryContentsManager(PantryContentsRepository PCR, ItemRepository IR, PantryRepository PR)
    {
        this.PR = PR;
        this.IR = IR;
        this.PCR = PCR;
    }

    public PantryContents addToPantry(PantryContents newPantryContent)
    {
        PantryContents IsItDuplicate = FindContentsByItemIDAndPantryID(newPantryContent.PCPantryID, newPantryContent.PCItemID);
        if (IsItDuplicate == null)
        {
            PCR.addToPantry(newPantryContent);
            return (newPantryContent);
        }
        else
        {
            IsItDuplicate.quantity += newPantryContent.quantity;
            pantryContentUpdate(IsItDuplicate);
            return IsItDuplicate;
        }
        
    }

    public IEnumerable<PantryContents> getAllPantryContents()
    {
        var results = PCR.GetAllPantryContents();
        results.Wait();
        return (results.Result);
    }

    public IEnumerable<PantryContents> FindContentsByPCPantryID(long PCPantryID)
    {
        var results = PCR.FindContentsByPCPantryID(PCPantryID);
        results.Wait();
        return (results.Result);
    }

    public PantryContents FindContentsByItemIDAndPantryID(long PCPantryID, long PCItemID)
    {
        var results = PCR.FindContentsByItemIDAndPantryID(PCPantryID, PCItemID);
        results.Wait();
        return (results.Result);
    }
    
    public IEnumerable<Item> WhatIsInThatPantry(long PCPantryID)
    {
        var contentsByPantryID = PCR.FindContentsByPCPantryID(PCPantryID);
        contentsByPantryID.Wait();
        var listOfPantryContents = contentsByPantryID.Result;
        foreach (PantryContents pantryContent in listOfPantryContents)
        {
            var findPantryItems = IR.FindItemByPrimaryKey(pantryContent.PCItemID);
            findPantryItems.Wait();
            var PantryItemList = findPantryItems.Result;
            PantryItemList.quantity = pantryContent.quantity;
            yield return PantryItemList;
        }
    }

    public IEnumerable<PantryItem> WhereIsThatItem(long PCItemID)
    {
        var contentsByItemID = PCR.FindContentsByPCItemID(PCItemID);
        contentsByItemID.Wait();
        var listOfItemContents = contentsByItemID.Result;
        foreach (PantryContents pantryContent in listOfItemContents)
        {
            var findPantryItems = PR.FindPantryByPrimaryKey(pantryContent.PCPantryID);
            findPantryItems.Wait();
            var PantriesContainingItem = findPantryItems.Result;
            PantryItem pantryItem = new PantryItem(PantriesContainingItem.pantryName, pantryContent.quantity, pantryContent.PantryContentID);
            yield return pantryItem;
        }
    }

    public void pantryContentUpdate(PantryContents updatedPantryContent)
    {
        PCR.pantryContentUpdate(updatedPantryContent);
    }

    public void deletePantryContent(long PantryContentID){
        PCR.deletePantryContent(PantryContentID);
    }

    public void deleteContentsByPantry(long PantryID)
    {
        PCR.deletePantryContentsByPantry(PantryID);
    }
}