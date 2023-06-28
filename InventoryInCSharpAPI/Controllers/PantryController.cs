using InventoryInCSharpAPI.Managers;
using InventoryInCSharpAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<Pantry> Get()
    {
        return pantryManager.GetPantryList();
    }
    [HttpGet("{primaryKey}")]
    public Pantry Get(long primaryKey)
    {
        return pantryManager.FindPantryByPrimaryKey(primaryKey);
    }

    [HttpGet("search/{searchValue}")]
    public IEnumerable<Pantry> Get(string searchValue)
    {
        return pantryManager.Search(searchValue);
    }
    [HttpPut("deletePantry/{pantryID}")]
    public void DeletePantry(long pantryID)
    {

        pantryManager.DeletePantry(pantryID);
    }

    [HttpPost]
    public Pantry Post([FromBody] Pantry postmanPantry)
    {
        return pantryManager.AddToPantryList(postmanPantry);
    }

    [HttpPut]
    public Pantry Put([FromBody] Pantry updatedPantry)
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
