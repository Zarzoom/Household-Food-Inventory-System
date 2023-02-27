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
    public IEnumerable<Pantry> Get() {
    return pantryManager.getPantryList();
    }
    [HttpGet("{primaryKey}")]
    public Pantry Get(long primaryKey)
    {
        return pantryManager.FindPantryByPrimaryKey(primaryKey);
    }

    [HttpGet("search/{searchValue}")]
    public IEnumerable<Pantry> Get(String searchValue)
    {
        return pantryManager.Search(searchValue);
    }
    [HttpPut("deletePantry/{PantryID}")]
    public void deletePantry(long PantryID){
        pantryManager.deletePantry(PantryID);
    }
    
    [HttpPost]
    public Pantry Post([FromBody] Pantry postmanPantry) {
    return pantryManager.addToPantryList(postmanPantry);
    }

    [HttpPut]
    public void Put([FromBody] Pantry updatedPantry)
    {
        pantryManager.pantryUpdate(updatedPantry);
    }
    
     //Commented out for safety. This method deletes ALL Items.
     //This method has not been tested.
     [HttpPut("deleteAllItems")]
     public void deleteALLItems(){
         pantryManager.deleteALLPantries();
     }
    }

