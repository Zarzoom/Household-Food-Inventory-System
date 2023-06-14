using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class LoginManager
{
    private readonly LoginRepository _LR;
    public LoginManager(LoginRepository LR)
    {
        this._LR = LR;
    }

    public User AddLogin(User newUser)
    {
        Boolean unique = NoDuplicates(newUser);
        if (unique == true)
        {
            var results = _LR.AddLogin(newUser);
            results.Wait();
            newUser.password = results.Result;
            return newUser;
        }
        else
        {
            return null;
        }
    }

    public Boolean NoDuplicates(User potentialUser)
    {
        var foundUserNames = findUserWithUserName(potentialUser);
        if (foundUserNames == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public User findUserWithUserName(User userForSearch)
    {
        var results = _LR.findUserWithUserName(userForSearch.userName);
        results.Wait();
        var foundUser = results.Result;
        return (foundUser);
    }

    public User findUserWithPassword(User userForSearch)
    {
        var results = _LR.findUserWithPassword(userForSearch.password);
        results.Wait();
        var foundUser = results.Result;
        return (foundUser);
    }
    
}
