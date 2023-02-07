using InventoryInCSharpAPI.Models;

namespace InventoryInCSharpAPI.Repositories;

public abstract class BaseRepository<Model> where Model: IModel
{
    public Model Add(Model model)
    { throw new NotImplementedException(); }
    public Model Remove(Model model) { throw new NotImplementedException(); }
    public Model Get(long primaryKey) { throw new NotImplementedException(); }

    public Model Search(String findMe) { throw new NotImplementedException(); }
    public Model Update(Model model) { throw new NotImplementedException(); }
}
