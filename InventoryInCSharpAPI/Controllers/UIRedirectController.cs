using Microsoft.AspNetCore.Mvc;

namespace InventoryInCSharpAPI.Controllers;

[Route("")]
[ApiController]
public class UIRedirectController: ControllerBase
{
    
    [HttpGet]
    public ContentResult Index()
    {
        var html = @"<head><meta http-equiv=""Refresh"" content=""0; URL=https://main.d3ogjmi84k9k7r.amplifyapp.com/"" /></head>";
        return new ContentResult
        {
            Content = html,
            ContentType = "text/html"
        };
    }
}
