namespace InventoryInCSharpAPI.Models;

public class User
{
    public String userName { get; set; }
    public long password { get; set; }

    public User()
    {
        
    }
    
    public User(String userName)
    {
        this.userName = userName;
    }
}