namespace DreamBook.API.Persistence;

public static class Extensions
{
    public static void UpdateDatabase(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<DreamBookBaseContext>();
        context.Database.Migrate();
    }
}
