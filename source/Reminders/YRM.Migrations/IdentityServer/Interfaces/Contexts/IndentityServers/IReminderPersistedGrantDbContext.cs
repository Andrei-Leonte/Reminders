using System.Threading.Tasks;

namespace YRM.Migrations.Interfaces.Contexts.IndentityServers
{
    internal interface IReminderPersistedGrantDbContext
    {
        Task MigrateAsync();
    }
}
