using EFCoreConsoleAppDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreConsoleAppDemo
{
    public class ApplicationDbContext : DbContext
    {     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-9F07U7R;Database=EFCoreConsoleAppDemoDBFelipe;Integrated Security=True");
        }

        public DbSet<Person> People { get; set; }
    }
}
