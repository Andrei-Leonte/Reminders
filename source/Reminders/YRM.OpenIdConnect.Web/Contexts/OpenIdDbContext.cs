using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YRM.Domain.Entities.Identity;

namespace YRM.OpenIdConnect.Web.Contexts
{
    internal class OpenIdDbContext : IdentityDbContext<ApplicationUser>
    {
        public OpenIdDbContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
