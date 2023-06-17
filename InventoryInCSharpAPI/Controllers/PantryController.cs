using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Managers;


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
    public IEnumerable<Pantry> GetAllPantries()
    {
        return pantryManager.GetPantryList();
    }
    [HttpGet("{primaryKey}")]
    public Pantry GetPantryWithPrimaryKey(long primaryKey)
    {
        return pantryManager.FindPantryByPrimaryKey(primaryKey);
    }

    [HttpGet("search/{searchValue}")]
    public IEnumerable<Pantry> GetPantryWithPantryName(String searchValue)
    {
        return pantryManager.Search(searchValue);
    }
    [HttpPut("deletePantry/{pantryID}")]
    public void DeletePantry(long pantryID)
    {

        pantryManager.DeletePantry(pantryID);
    }

    [HttpPost]
    public Pantry PostPantryToPantrList([FromBody] Pantry postmanPantry)
    {
        return pantryManager.AddToPantryList(postmanPantry);
    }

    [HttpPut]
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
