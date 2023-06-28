using InventoryInCSharpAPI.Managers;
using InventoryInCSharpAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryInCSharp.Controllers;

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
    /// <summary>
    ///     prices bellow 1 need a 0 in the ones place. example: 0.99
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<Item> Get()
    {
        return itemManager.GetItemList();
    }

    // GET api/<ItemController>/5
    [HttpGet("{primaryKey}")]
    public Item Get(long primaryKey)
    {
        return itemManager.FindByPrimaryKey(primaryKey);
    }
    //GET api/<ItemController>/search/searchValue
    [HttpGet("search/{searchValue}")]
    public IEnumerable<Item> Get(string searchValue)
    {
        return itemManager.Search(searchValue);
    }

    // POST api/<ItemController>
    [HttpPost]
    public Item Post([FromBody] Item postmanItem)
    {
        return itemManager.AddToItemList(postmanItem);
    }

    // PUT api/<ItemController>
    [HttpPut]
    public Item Put([FromBody] Item updatedItem)
    {
        return itemManager.ItemUpdate(updatedItem);
    }
    [HttpPut("deleteItem/{itemID}")]
    public void DeleteItem(long itemID)
    {
        itemManager.DeleteItem(itemID);
    }
    //Commented out for safety. This method deletes ALL Items.
    //This method has been manually tested. 
    // [HttpPut("deleteAllItems")]
    // public void deleteALLItems(){
    //     itemManager.deleteALLItems();
    // }
}
