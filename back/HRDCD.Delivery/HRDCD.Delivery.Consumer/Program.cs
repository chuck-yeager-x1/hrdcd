namespace HRDCD.Delivery.Consumer;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using HRDCD.Common.Tasks.Handlers;
using DataModel;
using HRDCD.Delivery.Tasks.DTO.Delivery;
using Tasks.DTO.Order;
using Tasks.Handlers.Order;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        var orderProcessingSection = config.GetSection("OrderProcessingSettings");

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<DeliveryDbContext>(options => { options.UseNpgsql(connectionString); });

        var host = Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.Populate(serviceCollection);

                builder.RegisterType<OrderCreateTaskHandler>()
                    .As<ITaskHandler<OrderCreateParam, OrderSelectTaskResult>>();
                builder.RegisterType<OrderStartDeliveryTaskHandler>()
                    .As<ITaskHandler<long, DeliveryStartTaskResult>>();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<OrderProcessingSettings>(orderProcessingSection);

                services.AddMassTransit(x =>
                {
                    x.AddConsumer<MessageConsumer>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(queueSettings.Hostname, h =>
                        {
                            h.Username(queueSettings.Username);
                            h.Password(queueSettings.Password);
                        });

                        cfg.ReceiveEndpoint(queueSettings.QueueName,
                            e => { e.ConfigureConsumer<MessageConsumer>(context); });
                    });
                });
            })
            .Build();

        await host.RunAsync();
    }
}