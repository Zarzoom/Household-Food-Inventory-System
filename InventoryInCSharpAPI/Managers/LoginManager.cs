using InventoryInCSharpAPI.Models;
using InventoryInCSharpAPI.Repositories;
namespace InventoryInCSharpAPI.Managers;

public class LoginManager
{
    private readonly LoginRepository _LR;
    private readonly ILogger<LoginManager> logger;
    public LoginManager(LoginRepository LR, ILogger<LoginManager> logger)
    {
        this._LR = LR;
        this.logger = logger;
    }

    public User AddLogin(User newUser)
    {
        var results = _LR.AddLogin(newUser);
            results.Wait();
            newUser.password = results.Result;
            return newUser;
    }

    public Boolean NoDuplicates(User potentialUser)
    {
        var foundUserName = findUserWithUserName(potentialUser.userName);
        logger.LogInformation($"{foundUserName?.userName} + {potentialUser.userName}");
        if (potentialUser.userName.Equals(foundUserName?.userName))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public User findUserWithUserName(String userNameForSearch)
    {
        var results = _LR.findUserWithUserName(userNameForSearch);
        results.Wait();
        var foundUser = results.Result;
        return (foundUser);
    }

    public User findUserWithPassword(long passwordForSearch)
    {
        var results = _LR.findUserWithPassword(passwordForSearch);
        results.Wait();
        var foundUser = results.Result;
        return (foundUser);
    }

    public User findUserWithPasswordAndUsername(User userForSearch)
    {
        var results = _LR.findUserWithPasswordAndUserName(userForSearch);
        results.Wait();
        var foundUser = results.Result;
        return (foundUser);
    }

    public User updateUserName(User updatedUser)
    {
        var foundUser = findUserWithPassword(updatedUser.password);
        
        var results = _LR.updateUserName(updatedUser);
            results.Wait();
            var createdUpdatedUser = results.Result;
            return createdUpdatedUser;
    }
}
