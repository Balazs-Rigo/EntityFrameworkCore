using System.ComponentModel.DataAnnotations;

namespace EFCoreMovies.DTO
{
    public class GenreCreationDTO
    {
        [Required]
        public string Name {get; set; }
    }
}
