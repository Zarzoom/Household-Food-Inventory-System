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
        return pantryContentsManager.addToPantry(postmanPantryContent);
    }

    [HttpGet]
    public IEnumerable<PantryContents> Get()
    {
        return pantryContentsManager.getAllPantryContents();
    }

    [HttpGet("{PCPantryID}")]
    public IEnumerable<PantryContents> Get(long PCPantryID)
    {
        return pantryContentsManager.FindContentsByPCPantryID(PCPantryID);
    }
    [HttpGet("{PCPantryID}/{PCItemID}")]
    public PantryContents Get(long PCPantryID, long PCItemID)
    {
        return pantryContentsManager.FindContentsByItemIDAndPantryID(PCPantryID, PCItemID);
    }

    [HttpGet("retrievePantry/{PantryID}")]
    public IEnumerable<Item> GetContentsFromPantry(long PantryID)
    {
        return pantryContentsManager.WhatIsInThatPantry(PantryID);
    }

    [HttpGet("retrieveItemLocation/{ItemID}")]
    public IEnumerable<PantryItem> findPantriesContainingItem(long ItemID)
    {
        return pantryContentsManager.WhereIsThatItem(ItemID);
    }

    [HttpPut("deletePantryContent/{PantryContentID}")]
    public void deletePantryContent(long PantryContentID){
        pantryContentsManager.deletePantryContent(PantryContentID);
    }

    [HttpPut]
    public void Put([FromBody] PantryContents updatedPantryContent)
    {
        pantryContentsManager.pantryContentUpdate(updatedPantryContent);
    }
}