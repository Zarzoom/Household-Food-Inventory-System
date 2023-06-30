using Dapper;
using Polly;
using Polly.Retry;
using System.Data;
using System.Data.SqlClient;
namespace InventoryInCSharpAPI.Services;

public static class DapperExtension
{
    private static readonly AsyncRetryPolicy RetryPolicy =
        Policy.Handle<SqlException>().Or<TimeoutException>().WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public async static Task<int> ExecuteAsyncWithRetry(this IDbConnection connector, string sql, object param = null,
        IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return (await RetryPolicy.ExecuteAsync(async () => { return (await connector.ExecuteAsync(sql, param, transaction, commandTimeout, commandType)); }));
    }

    public static async Task<IEnumerable<T>> QueryAsyncWithRetry<T>(this IDbConnection cnn, string sql, object param = null,
        IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await RetryPolicy.ExecuteAsync(async () => { return (await cnn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType));});
    }
}
