namespace HRDCD.Order.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Вспомогательный класс для манипуляций с моделью данных при разработке.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    /// <inheritdoc/>
    public OrderDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@Directory.GetCurrentDirectory() + "/../HRDCD.Order.Api/appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<OrderDbContext>();
        builder.UseNpgsql(configuration.GetConnectionString("Database"));

        return new OrderDbContext(builder.Options);
    }
}