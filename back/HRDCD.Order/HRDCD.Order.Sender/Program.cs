using Autofac;
using Autofac.Extensions.DependencyInjection;
using HRDCD.Common.Tasks.Handlers;
using HRDCD.Order.DataModel;
using HRDCD.Order.Tasks.DTO.Order;
using HRDCD.Order.Tasks.Handlers.Order;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HRDCD.Order.Sender;

class Program
{
    static async Task Main(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var config = configurationBuilder.Build();
        var connectionString = config.GetConnectionString("Database");
        
        var queueSection = config.GetSection("QueueSettings");
        var queueSettings = queueSection.Get<QueueSettings>();
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<OrderDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        var host = Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.Populate(serviceCollection);
                
                builder.Register(c =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        cfg.Host(new Uri(queueSettings.Hostname), host =>
                        {
                            host.Username(queueSettings.Username);
                            host.Password(queueSettings.Password);
                        });
                    });
                }).As<IBusControl>().SingleInstance();
                
                builder.RegisterType<OrderSelectFirstUnsentTaskHandler>().As<ITaskHandler<int, OrderSelectUnsentTaskResult>>().SingleInstance();
                builder.RegisterType<OrderMarkAsSentTaskHandler>().As<ITaskHandler<long, OrderSelectTaskResult>>().SingleInstance();
                
                builder.RegisterType<MessageSender>().As<IMessageSender>().SingleInstance();

                builder.RegisterType<Worker>().As<IHostedService>().SingleInstance();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<QueueSettings>(queueSection);
            })
            .Build();
        
        await host.RunAsync();
    }
}