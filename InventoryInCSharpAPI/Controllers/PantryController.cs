using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Managers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InventoryInCSharpAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PantryController : ControllerBase
{
    private readonly PantryManager pantryManager;

    public PantryController(PantryManager pantryManager)
    {
        this.pantryManager = pantryManager;
    }
    [HttpGet]
    [Authorize]
    public IEnumerable<Pantry> GetAllPantries()
    {
        return pantryManager.GetPantryList();
    }

    [HttpGet("userSearch/")]
    [Authorize]
    public IEnumerable<Pantry> GetUserPantries()
    {
        var user = this.User.Identity as ClaimsIdentity;
        var password = user.Claims.SingleOrDefault(m => m.Type == "sub").Value;
        return pantryManager.GetAllUserPantries(password);
    }

    [HttpGet("{primaryKey}")]
    [Authorize]
    public Pantry GetPantryWithPrimaryKey(long primaryKey)
    {
        return pantryManager.FindPantryByPrimaryKey(primaryKey);
    }

    [HttpGet("search/{searchValue}")]
    [Authorize]
    public IEnumerable<Pantry> GetPantryWithPantryName(String searchValue)
    {
        return pantryManager.Search(searchValue);
    }
    [HttpPut("deletePantry/{pantryID}")]
    [Authorize]
    public void DeletePantry(long pantryID)
    {

        pantryManager.DeletePantry(pantryID);
    }

    [HttpPost]
    [Authorize]
    public Pantry PostPantryToPantryList([FromBody] Pantry postmanPantry)
    {
        return pantryManager.AddToPantryList(postmanPantry);
    }

    [HttpPut]
    [Authorize]
    public Pantry PutUpdatesIntoPantryList([FromBody] Pantry updatedPantry)
    {
        return pantryManager.PantryUpdate(updatedPantry);
    }

    //Commented out for safety. This method deletes ALL Items.
    //This method has not been tested.
    // [HttpPut("DeleteAllPantries")]
    // public void DeleteALLItems(){
    //     pantryManager.DeleteALLPantries();
    // }
}
