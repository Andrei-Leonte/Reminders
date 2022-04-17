using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YRM.Migrations.Entities;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;

namespace YRM.Migrations.Contexts.IdentityServers
{
    internal class ReminderConfigurationDbContext :
        ConfigurationDbContext,
        IReminderConfigurationDbContext
    {
        private readonly IConfiguration configuration;
        public ReminderConfigurationDbContext(
            IConfiguration configuration,
            DbContextOptions<ConfigurationDbContext> options,
            ConfigurationStoreOptions storeOptions)
                : base(options, storeOptions)
        {
            this.configuration = configuration;
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }

        public async Task MigrateDefaultClientValuesAsync()
        {
            var clients = await Clients.ToListAsync();

            if (!clients.Any())
            {
                var client = new Client();
                var migrationReminderClientsSecret = new MigrationReminderClientsSecret();

                configuration.GetSection("ReminderClientsSecrets").Bind(migrationReminderClientsSecret);
                configuration.GetSection("ReminderClients").Bind(client);
                client.ClientSecrets = new List<ClientSecret>();

                client.ClientSecrets.Add(new ClientSecret()
                {
                    Value = migrationReminderClientsSecret.ClientSecret,
                    Description = "Default Client",
                    Expiration = DateTime.MaxValue
                });

                Clients.Add(client);

                await SaveChangesAsync();
            }
        }

        public async Task MigrateDefaultApiScopes()
        {
            var apiScopes = await ApiScopes.ToListAsync();

            if (!apiScopes.Any())
            {

                apiScopes = new List<ApiScope>();

                var apiScopesStrings = new List<string>();
                configuration.GetSection("ReminderApiScopes").Bind(apiScopesStrings);

                foreach (var apiScopesString in apiScopesStrings)
                {
                    apiScopes.Add(new ApiScope()
                    {
                        Enabled = true,
                        Name = apiScopesString,
                        DisplayName = $"Reminder - {apiScopesString}",
                        Description = $"Reminder - {apiScopesString}"
                    });
                }

                ApiScopes.AddRange(apiScopes);

                await SaveChangesAsync();
            }
        }

        public async Task MigrateDefaultIdentityResources()
        {
            var identityResources = await IdentityResources.ToListAsync();

            if (!identityResources.Any())
            {
                identityResources = new List<IdentityResource>()
                {
                   new IdentityResource()
                   {
                       Name = "openid",
                       DisplayName = "YRM.Reminder",
                       Required = true,
                       UserClaims = new List<IdentityResourceClaim>()
                       {
                           new IdentityResourceClaim()
                           {
                               Type = "sub"
                           }
                       }
                   }
                };

                IdentityResources.AddRange(identityResources);

                await SaveChangesAsync();
            }
        }
    }
}
