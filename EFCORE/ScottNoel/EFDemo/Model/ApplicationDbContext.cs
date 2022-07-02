using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ExpenseHeader> ExpenseHeaders { get; set; }
        public DbSet<ExpenseLine> ExpenseLines { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<ExpenseLine>()
                .Property(e => e.TotalCost)
                .HasComputedColumnSql("[Quantity] * [UnitCost]");
            builder.Entity<User>()
                .Property(p => p.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
