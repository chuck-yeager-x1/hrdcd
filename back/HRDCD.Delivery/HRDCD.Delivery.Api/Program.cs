using Autofac.Extensions.DependencyInjection;

namespace HRDCD.Delivery.Api;

/// <summary>
/// The main entry point for the HRDCD Order API application. This class is responsible for
/// configuring and starting the application's host.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point for the HRDCD Order API application. This class is responsible for
    /// configuring and starting the application's host.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Creates and configures an <see cref="IHostBuilder"/> for the HRDCD Order API application.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    /// <returns>An instance of <see cref="IHostBuilder"/> configured with the specified defaults.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}