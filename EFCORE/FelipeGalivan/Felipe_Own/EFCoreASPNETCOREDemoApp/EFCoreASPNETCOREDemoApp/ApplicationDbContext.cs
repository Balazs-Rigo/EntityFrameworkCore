using EFCoreASPNETCOREDemoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreASPNETCOREDemoApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
