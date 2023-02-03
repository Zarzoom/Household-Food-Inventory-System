﻿namespace InventoryInCSharp.Managers;
using InventoryInCSharp.Models;
using InventoryInCSharpAPI.Services;

public class ItemManager
{
    private ItemRepository IR { get; set; }
    public ItemManager(ItemRepository IR)
    {
        this.IR = IR;
    }
// private List<Item> ItemList { get; set; } = new List<Item>();

    public void addToItemList(Item newItem) {
        IR.AddToItemList(newItem);
        IR.SaveChanges();
    }

    public IEnumerable<Item> GetItemList()
    {
        return (IR.GetItemList());
    }
}
