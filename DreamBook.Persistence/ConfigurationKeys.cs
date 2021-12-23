using Microsoft.Extensions.Configuration;
using System;

public enum DBProvider
{
    SqlServer,
    MySql
}

public static class ConfigurationExtensions
{
    public static string GetDBConnectionString(this IConfiguration configuration)
    {
        return configuration[$"DB:ConnectionStrings:{configuration.GetDBProvider()}"];
    }

    public static string GetDBConnectionString(this IConfiguration configuration, DBProvider provider)
    {
        return configuration[$"DB:ConnectionStrings:{provider}"];
    }

    public static DBProvider GetDBProvider(this IConfiguration configuration)
    {
        if (Enum.TryParse(typeof(DBProvider), configuration["DB:Provider"], out object provider))
            return (DBProvider)provider;

        throw new InvalidOperationException("Invalid DB provider is selected");
    }
}
