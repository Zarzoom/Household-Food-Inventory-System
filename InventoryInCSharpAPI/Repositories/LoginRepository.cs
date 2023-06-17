using Dapper;
using InventoryInCSharpAPI.Models;
using MySqlConnector;
namespace InventoryInCSharpAPI.Repositories;
using System;

public class LoginRepository
{
    private ConnectionStringAndOtherSecrets CSOS;
    private readonly ILogger<LoginRepository> logger;


    public LoginRepository(ConnectionStringAndOtherSecrets csos, ILogger<LoginRepository> logger)
    {
        this.CSOS = csos;
        this.logger = logger;

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
        logger.LogInformation($"{userName}");
        Console.WriteLine($"{userName}");
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = $"SELECT UserName, Password FROM Login WHERE LOWER(UserName) = LOWER('{userName}')";
            var foundUser = await connection.QueryAsync<User>(sql);
            var singleUser = foundUser.FirstOrDefault();
            logger.LogInformation($"this {singleUser?.userName}");
            return foundUser.FirstOrDefault();
        }
    }
    
    public async Task<User> findUserWithPassword(long password)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var parameters = new { password };
            var sql = "SELECT UserName, Password FROM Login WHERE Password = @Password";
            var foundUser = await connection.QueryAsync<User>(sql, parameters);
            return foundUser.SingleOrDefault();
        }
    }

    public async Task<User> findUserWithPasswordAndUserName(User userForSearch)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var parameters = new { userForSearch };
            var sql = $"SELECT UserName, Password FROM Login WHERE Password = {userForSearch.password} AND UserName = {userForSearch.userName}";
            var foundUser = await connection.QueryAsync<User>(sql, parameters);
            return foundUser.SingleOrDefault();
        }
    }

    public async Task<User> updateUserName(User updatedUser)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var sql = "UPDATE Login SET UserName = @UserName WHERE Password = @Password; SELECT Password, UserName FROM Login WHERE Password = @Password";
            var createdUpdatedUser = await connection.QueryAsync<User>(sql, updatedUser);
            return createdUpdatedUser.SingleOrDefault();
        }
    }

    public async void DeleteUserWithUserName(String userNameForDeletion)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var parameters = new { userNameForDeletion};
            var sql = $"DELETE FROM Login Where UserName = @userNameForDeletion";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }

    public async void DeleteUserWithPassword(long password)
    {
        using (var connection = new MySqlConnection(CSOS.connection))
        {
            var parameters = new { password};
            var sql = $"DELETE FROM Login Where Password = @password";
            var deletedRows = await connection.ExecuteAsync(sql, parameters);
        }
    }
    
}
