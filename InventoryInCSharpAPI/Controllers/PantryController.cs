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

    [HttpPost]
    public Pantry Post([FromBody] Pantry postmanPantry) {
    return pantryManager.addToPantyList(postmanPantry);
    }

    [HttpPut]
    public Pantry Put([FromBody] Pantry updatedPantry)
    {
        return pantryManager.pantryUpdate(updatedPantry);
    }
    }

