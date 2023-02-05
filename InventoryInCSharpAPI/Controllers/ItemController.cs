﻿using Microsoft.AspNetCore.Mvc;
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
        return(itemManager.GetItemList());
    }

    // GET api/<ItemController>/5
    [HttpGet("{primaryKey}")]
    public Item Get(long primaryKey)
    {
        return itemManager.findByPrimaryKey(primaryKey);
    }
    // GET api/<ItemController>/search/searchValue
    [HttpGet("search/{searchValue}")]
    public IEnumerable<Item> Get(String searchValue)
    {
        return itemManager.Search(searchValue);
    }

    // POST api/<ItemController>
    [HttpPost]
    public Item Post([FromBody] Item postmanItem)
    {
        return itemManager.addToItemList(postmanItem);
    }

    // PUT api/<ItemController>
    [HttpPut]
    public Item Put([FromBody] Item updatedItem)
    {
        return itemManager.itemUpdate(updatedItem);
    }

    //// PUT api/<ItemController>/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] Item item)
    //{

    //}

    //// DELETE api/<ItemController>/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
}
