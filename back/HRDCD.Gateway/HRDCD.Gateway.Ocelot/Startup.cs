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

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

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