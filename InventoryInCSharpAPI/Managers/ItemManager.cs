using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class ItemManager
{
    private readonly ItemRepository _IR;
    private readonly PantryContentsRepository _PCR;

    public ItemManager(ItemRepository IR, PantryContentsRepository PCR)
    {
        this._IR = IR;
        this._PCR = PCR;
    }

    /// <summary>
    /// Takes in the Item that need to be added to the database and calls the addItem method from the repository.
    /// It then converts the response from the repository from a task<Item> to an Item.
    /// </summary>
    /// <param name="newItem">The new Item Object that needs to be added to the database.</param>
    /// <returns>Returns the inserted Item as an Item.</returns>
    public Item AddToItemList(Item newItem)
    {
        var results = _IR.AddToItemList(newItem);
        results.Wait();
        newItem.itemID = results.Result;
        return newItem;
    }

    /// <summary>
    /// Calls the GetItemList method (returns all Items stored in ItemList) from Item Repository and converts results from Task<IEnumerable<Item>> to IEnumerable<Item>.
    /// </summary>
    /// <returns>Returns a list of all of the Items in ItemList as IEnumerable<Item>.</ret
    public IEnumerable<Item> GetItemList()
    {
        var results = _IR.GetItemList();
        results.Wait();
        return (results.Result);
    }

    /// <summary>
    /// Calls the GetUserItems (finds all Items that match the users password) method from ItemRepository. Converts return from a Task<IEnumerable<item>> to an IEnumerable<Item> 
    /// </summary>
    /// <param name="password"> A long with a value that matches the users password.</param>
    /// <returns>Returns all Items that have a password that matches the parameter password.</returns>
    public IEnumerable<Item> GetAllUserItems(long password)
    {
        var results = _IR.GetUserItems(password);
        results.Wait();
        return (results.Result);
    }
    
    /// <summary>
    /// Calls the FindItemByPrimaryKey method (returns item whose primary key matches the parameter.) from Item Repository and converts results
    /// from Task<IEnumerable<Item>> to IEnumerable<Item>.
    /// </summary>
    /// <param name="primaryKey">takes in an integer that represents the primary key that will be passed to FindItemByPrimaryKey for the Item search.</param>
    /// <returns>Returns the Item that was found with the primary key as an Item. </returns>
    public Item FindByPrimaryKey(long primaryKey)
    {
        var results = _IR.FindItemByPrimaryKey(primaryKey);
        results.Wait();
        return (results.Result);
    }

    /// <summary>
    /// Calls the ContainsSearchForGenericNameAndBrand (Searches for an item whose GenericName and/or BrandName contains the string that was taken in as a parameter) method from Item Repository.
    /// This function will then convert the return from Task<IEnumerable<Item>> to IEnumerable<Item>
    /// </summary>
    /// <param name="findValue">A string that will be used as the search term when searching ItemList table.</param>
    /// <returns>Returns the Items, whose GenericName and/or BrandName contain the search string, as a list of Pantries.</returns>
    public IEnumerable<Item> Search(String findValue)
    {
        var results = _IR.ContainsSearchForGenericNameAndBrand(findValue);
        results.Wait();
        return (results.Result);
    }

    /// <summary>
    /// Calls the ItemUpdate (updates the item in the database) method from ItemRepository. Converts return from a Task<Item> to an Item> 
    /// </summary>
    /// <param name="updatedItem"> Item Object with the same primary key as the Item that needs the update but with edits to the other values</param>
    /// <returns>The Updated version of the Item as anItem</returns>
    public Item ItemUpdate(Item updatedItem)
    {
       var results =  _IR.ItemUpdate(updatedItem);
       results.Wait();
       return (results.Result);
    }

    /// <summary>
    /// Calls the DeleteContentsByItem(Deletes PantryContents that contain that ItemID as their PCItemID) method
    /// and the DeleteItem (Deletes the Item from ItemList)method from the ItemRepository.
    /// Calling DeleteContentsByItem makes sure that all pantryContents that are associated with the Item are deleted before the Item is deleted. 
    /// </summary>
    /// <param name="itemID">itemID is a long that represents the primary key of the Item that needs to be deleted.</param>
    public void DeleteItem(long itemID)
    {
        _PCR.DeletePantryContentsByItem(itemID);

        Task.Delay(500).Wait();
        _IR.DeleteItem(itemID);
    }
//TODO: find better option than task.delay

    /// <summary>
    /// Commented out in controller to prevent catastrophic accidents. This method should delete all of the Items from ItemList.
    /// There was very little manual testing of this method, and it does not have any integration testing.
    /// </summary>
    public void DeleteALLItems()
    {
        IEnumerable<Item> allItems = GetItemList();
        foreach (Item item in allItems)
        {
            DeleteItem(item.itemID);
        }
    }
}
