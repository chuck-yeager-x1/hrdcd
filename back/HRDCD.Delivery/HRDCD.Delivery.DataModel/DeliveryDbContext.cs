namespace HRDCD.Delivery.DataModel;

using Configuration;
using Entity;
using Microsoft.EntityFrameworkCore;

public class DeliveryDbContext : DbContext
{
    private readonly string? _connectionString;

    protected DeliveryDbContext(string? connectionString) => _connectionString = connectionString;

    public DeliveryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DeliveryEntity> Deliveries { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(this._connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfig());
        modelBuilder.ApplyConfiguration(new DeliveryConfig());
    }
}