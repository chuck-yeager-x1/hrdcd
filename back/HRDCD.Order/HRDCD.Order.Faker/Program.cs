using HRDCD.Order.DataModel;
using HRDCD.Order.DataModel.Entity;
using Microsoft.Extensions.Configuration;

namespace HRDCD.Order.Faker;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
		
        var config = builder.Build();

        var connectionString = config.GetConnectionString("Database");
        using var db = new OrderDbContext(connectionString);

        var testOrders = new Bogus.Faker<OrderEntity>("ru")
            .RuleFor(_ => _.OrderName, f => f.Commerce.ProductName())
            .RuleFor(_ => _.OrderNumber, f => f.Commerce.Ean13())
            .RuleFor(_ => _.OrderDescription, f => f.Lorem.Sentences(20))
            .RuleFor(_ => _.InsertDate, DateTime.UtcNow)
            .RuleFor(_ => _.UpdateDate, DateTime.UtcNow)
            .RuleFor(_ => _.DeleteDate, DateTime.UtcNow)
            .RuleFor(_ => _.IsDeleted, false)
            .RuleFor(_ => _.IsSent, false);
            
        var orders = testOrders.Generate(1000);
        db.Set<OrderEntity>().AddRange(orders);
        db.SaveChanges();
    }
}