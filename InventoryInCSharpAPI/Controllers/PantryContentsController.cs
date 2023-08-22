using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Managers;

namespace InventoryInCSharpAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PantryContentsController : ControllerBase
{
    private readonly PantryContentsManager pantryContentsManager;

    public PantryContentsController(PantryContentsManager pantryContentsManager)
    {
        this.pantryContentsManager = pantryContentsManager;
    }

    [HttpPost]
    public PantryContents PostNewPantryContent([FromBody] PantryContents postmanPantryContent)
    {
        return pantryContentsManager.AddToPantry(postmanPantryContent);
    }

    [HttpGet]
    public IEnumerable<PantryContents> GetAllPantryContents()
    {
        return pantryContentsManager.GetAllPantryContents();
    }
    
    [HttpGet("userSearch/{password}")]
    public IEnumerable<PantryContents> GetUserPantryContents(long password)
    {
        return pantryContentsManager.GetAllUserPantryContents(password);
    }

    [HttpGet("{pcPantryID}")]
    public IEnumerable<PantryContents> GetPantryContentsWithPantryID(long pcPantryID)
    {
        return pantryContentsManager.FindContentsByPCPantryID(pcPantryID);
    }
    [HttpGet("{pcPantryID}/{pcItemID}")]
    public PantryContents GetPantryContentsWithPantryIDAndItemID(long pcPantryID, long pcItemID)
    {
        return pantryContentsManager.FindContentsByItemIDAndPantryID(pcPantryID, pcItemID);
    }

    [HttpGet("retrieveItemsFromPantry/{pantryID}")]
    public IEnumerable<Item> GetContentsFromPantry(long pantryID)
    {
        return pantryContentsManager.WhatIsInThatPantry(pantryID);
    }

    [HttpGet("retrieveItemLocation/{itemID}")]
    public IEnumerable<PantryItem> findPantriesContainingItem(long itemID)
    {
        return pantryContentsManager.WhereIsThatItem(itemID);
    }
  
    [HttpGet("retrievePantryContentsWithItem/{itemID}")]
    public IEnumerable<PantryContents> findPantryContentsContainingItem(long itemID)
    {
        return pantryContentsManager.FindContentsByPCItemID(itemID);
    }
    

    [HttpPut("deletePantryContent/{pantryContentID}")]
    public void DeletePantryContent(long pantryContentID)
    {
        pantryContentsManager.DeletePantryContent(pantryContentID);
    }

    [HttpPut]
    public PantryContents PutUpdatedPantryContentIntoPantryContentsList([FromBody] PantryContents updatedPantryContent)
    {
        return pantryContentsManager.PantryContentUpdate(updatedPantryContent);
    }
}
