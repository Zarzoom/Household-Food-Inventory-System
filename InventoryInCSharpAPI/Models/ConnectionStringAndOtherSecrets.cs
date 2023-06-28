namespace InventoryInCSharpAPI.Models;

public class ConnectionStringAndOtherSecrets
{

    public ConnectionStringAndOtherSecrets(string setConnect)
    {
        connection = setConnect;
    }
    public string connection { get; set; }
}
