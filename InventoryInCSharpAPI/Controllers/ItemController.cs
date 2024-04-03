using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Managers;
using Microsoft.AspNetCore.Authorization;

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
    /// prices bellow 1 need a 0 in the ones place. example: 0.99
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public IEnumerable<Item> GetAllItems()
    {
        return (itemManager.GetItemList());
    }

    // GET api/<ItemController>/5
    [HttpGet("{primaryKey}")]
    [Authorize]
    public Item GetItemWithPrimaryKey(long primaryKey)
    {
        return itemManager.FindByPrimaryKey(primaryKey);
    }
    
    [HttpGet("userSearch/")]
    [Authorize]
    public IEnumerable<Item> GetUserItems()
    {
        var user = this.User.Identity as ClaimsIdentity;
        var password = user.Claims.SingleOrDefault(m => m.Type == "sub").Value;
        return itemManager.GetAllUserItems(password);
    }
    
    //GET api/<ItemController>/search/searchValue
    [HttpGet("search/{searchValue}")]
    [Authorize]
    public IEnumerable<Item> Get(String searchValue)
    {
        return itemManager.Search(searchValue);
    }

    // POST api/<ItemController>
    [HttpPost]
    [Authorize]
    public Item PostItemToItemList([FromBody] Item postmanItem)
    {
        return itemManager.AddToItemList(postmanItem);
    }

    // PUT api/<ItemController>
    [HttpPut]
    [Authorize]
    public Item PutUpdatedItemIntoDatabase([FromBody] Item updatedItem)
    {
        return itemManager.ItemUpdate(updatedItem);
    }
    
    [HttpPut("deleteItem/{itemID}")]
    [Authorize]
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
