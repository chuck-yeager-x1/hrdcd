using HRDCD.Delivery.DataModel;

namespace HRDCD.Delivery.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Вспомогательный класс для манипуляций с моделью данных при разработке.
/// </summary>
// ReSharper disable UnusedType.Global
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeliveryDbContext>
// ReSharper restore UnusedType.Global
{
    /// <inheritdoc/>
    public DeliveryDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@Directory.GetCurrentDirectory() + "/../HRDCD.Delivery.Api/appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<DeliveryDbContext>();
        builder.UseNpgsql(configuration.GetConnectionString("Database"), _ => _.MigrationsAssembly("HRDCD.Delivery.Database"));

        return new DeliveryDbContext(builder.Options);
    }
}