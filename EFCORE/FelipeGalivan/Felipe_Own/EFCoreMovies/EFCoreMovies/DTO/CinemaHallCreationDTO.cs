using EFCoreMovies.Entities;

namespace EFCoreMovies.DTO
{
    public class CinemaHallCreationDTO
    {
        public double Cost { get; set; }
        public CinemaHallType CinemaHallType { get; set; }
    }
}
