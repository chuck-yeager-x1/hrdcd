using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace HRDCD.Gateway.Ocelot;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        this.Configuration = configuration;
        this.Environment = environment;
    }

    /// <summary>
    /// Gets the configuration settings for the application.
    /// This property is used to retrieve various configuration values
    /// from the application's configuration files, such as appsettings.json or environment variables.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Gets the hosting environment for the application.
    /// This property provides information about the web hosting environment
    /// in which the application is running, such as Development, Staging, or Production.
    /// </summary>
    public IWebHostEnvironment Environment { get; }

    /// <summary>
    /// <inheritdoc cref=""/>
    /// </summary>
    /// <param name="services"><inheritdoc cref=""/>123.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddRouting(opts => { opts.LowercaseUrls = true; });

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors();
        
        services.AddOcelot(Configuration);

        // services.AddAutoMapper(typeof(MappingProfile));
    }

    /// <summary>
    /// <inheritdoc cref=""/>
    /// </summary>
    /// <param name="app"><inheritdoc cref=""/>456.</param>
    public void Configure(IApplicationBuilder app)
    {
        if (this.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            // для локального запуска
            app.UseCors(_ => _.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseOcelot();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}