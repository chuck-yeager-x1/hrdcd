namespace HRDCD.Delivery.Api;

using Autofac;
using DataModel;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        this.Configuration = configuration;
        this.Environment = environment;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new AutofacModule());
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DeliveryDbContext>(
            opts => { opts.UseNpgsql(this.Configuration.GetConnectionString("Database")); }, ServiceLifetime.Scoped);

        services.AddHttpContextAccessor();
        services.AddRouting(opts => { opts.LowercaseUrls = true; });

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors();

        // services.AddAutoMapper(typeof(MappingProfile));
    }

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

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}