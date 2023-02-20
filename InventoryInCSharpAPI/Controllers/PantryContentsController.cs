using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Managers;

namespace InventoryInCSharpAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]

    public class PantryContentsController: ControllerBase
    {
        private readonly PantryContentsManager pantryContentsManager;
       
       public PantryContentsController(PantryContentsManager pantryContentsManager){
        this.pantryContentsManager = pantryContentsManager;
       }

    [HttpPost]
    public PantryContents Post([FromBody] PantryContents postmanPantryContent) {
    return pantryContentsManager.addToPantry(postmanPantryContent);
    }

    [HttpGet]
    public IEnumerable<PantryContents> Get() 
    {
    return pantryContentsManager.getAllPantryContents();
    }
}