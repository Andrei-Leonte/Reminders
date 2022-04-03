using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using YRM.Domain.Entities.Identity;
using YRM.Infrastructure.Contexts;
using YRM.OpenIdConnect.Web;
using YRM.OpenIdConnect.Web.Contexts;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static OpenIddict.Server.OpenIddictServerEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("ReminderDBConnectionString");

builder.Services.AddDbContext<OpenIdDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseOpenIddict();
});

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<OpenIdDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = Claims.Role;
    options.ClaimsIdentity.EmailClaimType = Claims.Email;
});

builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});

builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

builder.Services.AddOpenIddict()

    // Register the OpenIddict core components.
    .AddCore(options =>
    {
        // Configure OpenIddict to use the Entity Framework Core stores and models.
        // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
        options.UseEntityFrameworkCore()
       .UseDbContext<OpenIdDbContext>();

        // Developers who prefer using MongoDB can remove the previous lines
        // and configure OpenIddict to use the specified MongoDB database:
        // options.UseMongoDb()
        //        .UseDatabase(new MongoClient().GetDatabase("openiddict"));

        // Enable Quartz.NET integration.
        options.UseQuartz();
    })

    // Register the OpenIddict server components.
    .AddServer(options =>
    {
        // Enable the authorization, device, logout, token, userinfo and verification endpoints.
        options.SetAuthorizationEndpointUris("/connect/authorize")
       .SetDeviceEndpointUris("/connect/device")
       .SetLogoutEndpointUris("/connect/logout")
       .SetTokenEndpointUris("/connect/token")
       .SetUserinfoEndpointUris("/connect/userinfo")
       .SetVerificationEndpointUris("/connect/verify");

        // Note: this sample uses the code, device code, password and refresh token flows, but you
        // can enable the other flows if you need to support implicit or client credentials.
        options.AllowAuthorizationCodeFlow()
       .AllowDeviceCodeFlow()
       .AllowPasswordFlow()
       .AllowRefreshTokenFlow();

        // Mark the "email", "profile", "roles" and "demo_api" scopes as supported scopes.
        options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "demo_api");

        // Register the signing and encryption credentials.
        options.AddDevelopmentEncryptionCertificate()
       .AddDevelopmentSigningCertificate();

        // Force client applications to use Proof Key for Code Exchange (PKCE).
        options.RequireProofKeyForCodeExchange();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore()
       .EnableStatusCodePagesIntegration()
       .EnableAuthorizationEndpointPassthrough()
       .EnableLogoutEndpointPassthrough()
       .EnableTokenEndpointPassthrough()
       .EnableUserinfoEndpointPassthrough()
       .EnableVerificationEndpointPassthrough()
       .DisableTransportSecurityRequirement(); // During development, you can disable the HTTPS requirement.

        // Note: if you don't want to specify a client_id when sending
        // a token or revocation request, uncomment the following line:
        //
        // options.AcceptAnonymousClients();

        // Note: if you want to process authorization and token requests
        // that specify non-registered scopes, uncomment the following line:
        //
        // options.DisableScopeValidation();

        // Note: if you don't want to use permissions, you can disable
        // permission enforcement by uncommenting the following lines:
        //
        // options.IgnoreEndpointPermissions()
        //        .IgnoreGrantTypePermissions()
        //        .IgnoreResponseTypePermissions()
        //        .IgnoreScopePermissions();

        // Note: when issuing access tokens used by third-party APIs
        // you don't own, you can disable access token encryption:
        //
        // options.DisableAccessTokenEncryption();
    })

    // Register the OpenIddict validation components.
    .AddValidation(options =>
    {
        // Configure the audience accepted by this resource server.
        // The value MUST match the audience associated with the
        // "demo_api" scope, which is used by ResourceController.
        options.AddAudiences("resource_server");

        // Import the configuration from the local OpenIddict server instance.
        options.UseLocalServer();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();

        // For applications that need immediate access token or authorization
        // revocation, the database entry of the received tokens and their
        // associated authorizations can be validated for each API call.
        // Enabling these options may have a negative impact on performance.
        //
        // options.EnableAuthorizationEntryValidation();
        // options.EnableTokenEntryValidation();
    });


builder.Services.AddHostedService<Worker>();

//builder.Services.AddOpenIddict<OpenIdDbContext, long>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
