using HRDCD.Delivery.DataModel.Configuration;
using HRDCD.Delivery.DataModel.Entity;
using Microsoft.EntityFrameworkCore;

namespace HRDCD.Delivery.DataModel;

public class DeliveryDbContext : DbContext
{
    private readonly string _connectionString;

    protected DeliveryDbContext(string connectionString) => _connectionString = connectionString;

    public DeliveryDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<DeliveryEntity> Deliveries { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    
    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(this._connectionString);
        }
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfig());
        modelBuilder.ApplyConfiguration(new DeliveryConfig());
    }
}