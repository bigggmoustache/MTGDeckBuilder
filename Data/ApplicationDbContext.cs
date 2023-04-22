using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MTGDeckBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ExternalAuthInfo> ExternalAuthInfos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}