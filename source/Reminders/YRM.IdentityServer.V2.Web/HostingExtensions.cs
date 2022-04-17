using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using YRM.IdentityServer.Domain.Entities.Identity;
using YRM.IdentityServer.Infrastructure.Contexts;

namespace IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration["ReminderDBConnectionString"];

        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<AspIdentityDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AspIdentityDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString);
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(connectionString);
            });

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
            
        app.UseIdentityServer();

        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }
}