using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YRM.IdentityServer.Domain.Entities.Identity;

namespace YRM.IdentityServer.Infrastructure.Contexts
{
    internal class AspIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AspIdentityDbContext(DbContextOptions<AspIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
