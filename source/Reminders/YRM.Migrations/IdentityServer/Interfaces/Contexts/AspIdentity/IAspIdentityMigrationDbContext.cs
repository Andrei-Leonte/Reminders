using System.Threading.Tasks;

namespace YRM.Migrations.Interfaces.Contexts.AspIdentity
{
    internal interface IAspIdentityMigrationDbContext
    {
        Task MigrateAsync();
        Task MigrateDefaultAspUserAsync();
    }
}
