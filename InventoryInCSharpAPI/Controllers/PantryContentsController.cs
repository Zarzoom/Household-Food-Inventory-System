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
    public PantryContents Post([FromBody] PantryContents postmanPantryContent)
    {
        return pantryContentsManager.AddToPantry(postmanPantryContent);
    }

    [HttpGet]
    public IEnumerable<PantryContents> Get()
    {
        return pantryContentsManager.GetAllPantryContents();
    }

    [HttpGet("{pcPantryID}")]
    public IEnumerable<PantryContents> Get(long pcPantryID)
    {
        return pantryContentsManager.FindContentsByPCPantryID(pcPantryID);
    }
    [HttpGet("{pcPantryID}/{pcItemID}")]
    public PantryContents Get(long pcPantryID, long pcItemID)
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

    [HttpPut("deletePantryContent/{pantryContentID}")]
    public void DeletePantryContent(long pantryContentID)
    {
        pantryContentsManager.DeletePantryContent(pantryContentID);
    }

    [HttpPut]
    public void Put([FromBody] PantryContents updatedPantryContent)
    {
        pantryContentsManager.PantryContentUpdate(updatedPantryContent);
    }
}
