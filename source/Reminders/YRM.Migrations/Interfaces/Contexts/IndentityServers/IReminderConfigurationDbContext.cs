﻿using System.Threading.Tasks;

namespace YRM.Migrations.Interfaces.Contexts.IndentityServers
{
    internal interface IReminderConfigurationDbContext
    {
        Task MigrateAsync();
    }
}
