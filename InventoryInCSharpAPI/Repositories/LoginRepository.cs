using Dapper;
using InventoryInCSharpAPI.Models;
using MySqlConnector;
namespace InventoryInCSharpAPI.Repositories;

public class LoginRepository
{
    private ConnectionStringAndOtherSecrets CSOS;

    public LoginRepository(ConnectionStringAndOtherSecrets csos)
    {
        this.CSOS = csos;
    }
    
    public async Task<int> AddLogin(User user)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = "INSERT INTO Login (UserName) VALUES (@UserName); SELECT LAST_INSERT_ID()";
            var createdLogin = await connection.QueryAsync<int>(sql, user);
            return createdLogin.SingleOrDefault();
        }
    }

    public async Task<User> findUserWithUserName(String userName)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = "SELECT UserName FROM Login WHERE LOWER(UserName) = LOWER('%{userName}%')";
            var foundUser = await connection.QueryAsync<User>(sql);
            return foundUser.SingleOrDefault();
        }
    }
    
    public async Task<User> findUserWithPassword(int password)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = "SELECT UserName FROM Login WHERE Password= @Password";
            var foundUser = await connection.QueryAsync<User>(sql);
            return foundUser.SingleOrDefault();
        }
    }
}
