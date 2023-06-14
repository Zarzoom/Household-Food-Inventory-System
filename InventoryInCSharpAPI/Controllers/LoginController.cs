using Microsoft.AspNetCore.Mvc;
using InventoryInCSharpAPI.Managers;
using InventoryInCSharpAPI.Models;

namespace InventoryInCSharpAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly LoginManager loginManager;
    public LoginController()
    {
        this.loginManager = loginManager;
    }

    [HttpPost]

    public User Post([FromBody] User postmanUser)
    {
        return loginManager.AddLogin(postmanUser);
    }

    [HttpGet("search/{userName}")]

    public User Get(User)
    {
        
    }
    
}
