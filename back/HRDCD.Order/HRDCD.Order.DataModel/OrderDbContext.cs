namespace HRDCD.Order.DataModel;

using Configuration;
using Microsoft.EntityFrameworkCore;

public class OrderDbContext : DbContext
{
    private readonly string? _connectionString;

    public OrderDbContext(string? connectionString) => _connectionString = connectionString;

    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    public DbSet<Entity.OrderEntity> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfig());
    }
}