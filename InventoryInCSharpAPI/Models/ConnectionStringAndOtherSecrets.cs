namespace InventoryInCSharpAPI.Models;

public class ConnectionStringAndOtherSecrets
{
    public String connection { get; set; }

    public ConnectionStringAndOtherSecrets(String setConnect)
    {
        connection = setConnect;
    }
}
