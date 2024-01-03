


using Task2_api.Middleware;
using Task2_api.Services;

namespace Task2_api;

public class Startup
{
    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<ILoggerService,ConsoleLogger>(); //Added logger service with singleton lifetime

    }
    
    public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseCustomExceptionMiddle();
        
        app.UseEndpoints(x => { x.MapControllers(); });
    }
}