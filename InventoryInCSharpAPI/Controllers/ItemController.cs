using Microsoft.AspNetCore.Mvc;
using InventoryInCSharp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryInCSharp.Controllers;

using InventoryInCSharp.Managers;
[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemManager itemManager;

    public ItemController(ItemManager itemManager)
    {
        this.itemManager = itemManager;
    }
    // GET: api/<ItemController>
    [HttpGet]
    public IEnumerable<Item> Get()
    {
        return new List <Item> ();
    }

    // GET api/<ItemController>/5
    [HttpGet("sdfghjk/{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ItemController>
    [HttpPost]
    public void Post([FromBody] Item postmanItem)
    {
        itemManager.addToItemList(postmanItem);
    }

    // PUT api/<ItemController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Item item)
    {

    }

    // DELETE api/<ItemController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
