namespace HRDCD.Order.DataModel;

using Configuration;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контекст подключения к БД для работы с данными заказов.
/// </summary>
public class OrderDbContext : DbContext
{
    private readonly string _connectionString;

    /// <summary>
    /// Инициализация нового экземпляра <see cref="OrderDbContext"/> со строкой подключения.
    /// </summary>
    /// <param name="connectionString">Строка подключения к БД.</param>
    public OrderDbContext(string connectionString) => this._connectionString = connectionString;

    /// <summary>
    /// Инициализация нового экземпляра <see cref="OrderDbContext"/>.
    /// </summary>
    /// <param name="options"><inheritdoc cref="options"/></param>
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Возвращает или задает <see cref="DbSet{TEntity}"/> для работы с сущностями заказов <see cref="Order"/>.
    /// </summary>
    public DbSet<DataModel.Entity.OrderEntity> Orders { get; set; }

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
    }
}