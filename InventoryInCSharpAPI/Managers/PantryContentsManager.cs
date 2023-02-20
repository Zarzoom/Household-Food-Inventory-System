using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers
{
    public class PantryContentsManager
    {
    private PantryContentsRepository PCR {get; set;}
    public PantryContentsManager(PantryContentsRepository PCR)
    {
        this.PCR = PCR;
    }

    public PantryContents addToPantry(PantryContents newPantryContent){
        PCR.addToPantry(newPantryContent);
        return(newPantryContent);
    }

    public IEnumerable<PantryContents> getAllPantryContents()
    {
        var results = PCR.GetAllPantryContents();
        results.Wait();
        return(results.Result);
    }
    }
}
