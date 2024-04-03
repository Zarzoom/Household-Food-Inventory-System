using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Managers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
    [Authorize]
    public PantryContents PostNewPantryContent([FromBody] PantryContents postmanPantryContent)
    {
        return pantryContentsManager.AddToPantry(postmanPantryContent);
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<PantryContents> GetAllPantryContents()
    {
        return pantryContentsManager.GetAllPantryContents();
    }
    
    [HttpGet("userSearch/")]
    [Authorize]
    public IEnumerable<PantryContents> GetUserPantryContents()
    {
        var user = this.User.Identity as ClaimsIdentity;
        var password = user.Claims.SingleOrDefault(m => m.Type == "sub").Value;
        return pantryContentsManager.GetAllUserPantryContents(password);
    }

    [HttpGet("{pcPantryID}")]
    [Authorize]
    public IEnumerable<PantryContents> GetPantryContentsWithPantryID(long pcPantryID)
    {
        return pantryContentsManager.FindContentsByPCPantryID(pcPantryID);
    }
    [HttpGet("{pcPantryID}/{pcItemID}")]
    [Authorize]
    public PantryContents GetPantryContentsWithPantryIDAndItemID(long pcPantryID, long pcItemID)
    {
        return pantryContentsManager.FindContentsByItemIDAndPantryID(pcPantryID, pcItemID);
    }

    [HttpGet("retrieveItemsFromPantry/{pantryID}")]
    [Authorize]
    public IEnumerable<Item> GetContentsFromPantry(long pantryID)
    {
        return pantryContentsManager.WhatIsInThatPantry(pantryID);
    }

    [HttpGet("retrieveItemLocation/{itemID}")]
    [Authorize]
    public IEnumerable<PantryItem> findPantriesContainingItem(long itemID)
    {
        return pantryContentsManager.WhereIsThatItem(itemID);
    }
  
    [HttpGet("retrievePantryContentsWithItem/{itemID}")]
    [Authorize]
    public IEnumerable<PantryContents> findPantryContentsContainingItem(long itemID)
    {
        return pantryContentsManager.FindContentsByPCItemID(itemID);
    }
    

    [HttpPut("deletePantryContent/{pantryContentID}")]
    [Authorize]
    public void DeletePantryContent(long pantryContentID)
    {
        pantryContentsManager.DeletePantryContent(pantryContentID);
    }

    [HttpPut]
    [Authorize]
    public PantryContents PutUpdatedPantryContentIntoPantryContentsList([FromBody] PantryContents updatedPantryContent)
    {
        return pantryContentsManager.PantryContentUpdate(updatedPantryContent);
    }
}
