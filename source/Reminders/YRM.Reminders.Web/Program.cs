using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using YRM.Common.Entities;
using YRM.Reminders.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

var azureConfiguration = new AzureConfiguration();
builder.Configuration.GetSection("AzureConfiguration").Bind(azureConfiguration);

Environment.SetEnvironmentVariable("AZURE_TENANT_ID", azureConfiguration.AzureDirectoryId);
Environment.SetEnvironmentVariable("AZURE_CLIENT_ID", azureConfiguration.AzureAADClientId);
Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", azureConfiguration.AzureAADClientSecret);

builder.Configuration.AddAzureKeyVault(
    new Uri(azureConfiguration.GetKeyVaultUrl()),
    new DefaultAzureCredential(new DefaultAzureCredentialOptions()
    {
        ManagedIdentityClientId = "94479e63-eb0f-4736-b3e5-f513dc9f7f07",
        ExcludeSharedTokenCacheCredential = true,
        VisualStudioTenantId = azureConfiguration.AzureDirectoryId
    }));

var connectionString  = builder.Configuration["ReminderDBConnectionString"];

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7062";
        options.TokenValidationParameters.ValidateAudience = false;

        // options.Audience = "https://localhost:7181";

        //options.MapInboundClaims = false;

        //options.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidateAudience = true,
        //    ValidateIssuer = true
        //};
    });



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReminderDbContext>(options => options.UseSqlServer(connectionString));

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
