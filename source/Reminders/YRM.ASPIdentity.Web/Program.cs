using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YRM.ASPIdentity.Application;
using YRM.ASPIdentity.Application.Entities.JWTs;
using YRM.Common;
using YRM.Domain.Entities.Identity;
using YRM.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var jwtBearer = new JWTBearer();

var connectionString = builder.Configuration.GetConnectionString("ReminderDBConnectionString");
builder.Configuration.GetSection("JWTBearer").Bind(jwtBearer);

builder.Services
    .AddDbContext<AspIdentityDbContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AspIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication()
    .AddIdentityServerJwt();

builder.Services
    .AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtBearer.Issuer,
            ValidAudiences = jwtBearer.GetValidAudiences(),
            IssuerSigningKeys = jwtBearer.GetSecurityKeys(),
        };
    });


builder.Services.Configure<JwtBearerOptions>(
    IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
    options =>
    {
        var onTokenValidated = options.Events.OnTokenValidated;

        options.Events.OnTokenValidated = async context =>
        {
            await onTokenValidated(context);
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.RegisterCommonPackages();
builder.Services.RegistersApplicationPackages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
