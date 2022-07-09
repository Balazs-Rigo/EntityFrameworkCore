using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Entities.Seeding
{
    public static class Module3Seeding
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var action = new Genre { Id = 1, Name = "Action" };

            modelBuilder.Entity<Genre>().HasData(action);
        }
    }
}
