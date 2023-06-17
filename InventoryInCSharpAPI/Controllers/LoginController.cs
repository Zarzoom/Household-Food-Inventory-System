using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Managers;
using InventoryInCSharpAPI.Models;

namespace InventoryInCSharpAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly LoginManager loginManager;
    public LoginController(LoginManager loginManager)
    {
        this.loginManager = loginManager;
    }

    [HttpPost]

    public IActionResult PostUserToLogin([FromBody] User postmanUser)
    {
        if (loginManager.NoDuplicates(postmanUser) == true)
        {
            return Ok(loginManager.AddLogin(postmanUser));
        }
        else
        {
            return (Conflict("The user name has already been taken. Please, choose another."));
        }
    }

    [HttpGet("usernameSearch/{userName}")]

    public User GetUserWithUserName(String userName)
    {
        return (loginManager.findUserWithUserName(userName));
    }

    [HttpGet("passwordSearch/{password}")]

    public User GetUserWithPassword(long password)
    {
        return (loginManager.findUserWithPassword(password));
    }

    [HttpPut("LoginSearch")]

    public IActionResult SearchWithUserNameAndPassword([FromBody] User searchUser)
    {
        var searchResult = loginManager.findUserWithPasswordAndUsername(searchUser);
        if (searchResult == null)
        {
            return (NotFound("User name or password is incorrect."));
        }
        else
        {
            return (Ok(true));
        }
    }

    [HttpPut("updateUserName")]

    public User PutUpdatedUserNameIntoEffect([FromBody] User updatedUser)
    {
        return (loginManager.updateUserName(updatedUser));
    }
    
}
