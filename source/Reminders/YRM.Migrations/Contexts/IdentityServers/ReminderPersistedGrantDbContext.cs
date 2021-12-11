using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using YRM.Migrations.Entities;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;

namespace YRM.Migrations.Contexts.IdentityServers
{
    internal class ReminderPersistedGrantDbContext : 
        PersistedGrantDbContext, IReminderPersistedGrantDbContext
    {
        private readonly IConfiguration configuration;

        public ReminderPersistedGrantDbContext(
            IConfiguration configuration,
            DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions)
            : base(options, storeOptions)
        {
            this.configuration = configuration;
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }

    }
}