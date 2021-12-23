using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static string GetDBConnectionString(this IConfiguration configuration) =>
        configuration["ConnectionStrings:DreamBookConnectionSqlSqlServer"];
}
