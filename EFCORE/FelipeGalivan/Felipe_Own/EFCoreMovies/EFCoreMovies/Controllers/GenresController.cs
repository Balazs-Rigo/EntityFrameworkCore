using AutoMapper;
using EFCoreMovies.DTO;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController :ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {
            _context.Logs.Add(new Log { Message = "Executing Get from GenresController" });

            return await _context.Genres.AsNoTracking()
                .OrderBy(g=>g.Name)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(GenreCreationDTO genreCreationDTO)
        {
            var genreExists = await _context.Genres.AnyAsync(p => p.Name == genreCreationDTO.Name);

            if (genreExists)
            {
                return BadRequest($"The genre with name {genreCreationDTO.Name} already exists");
            }

            var genre = _mapper.Map<Genre>(genreCreationDTO);
            _context.Add(genre); 

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("several")]
        public async Task<ActionResult> Post(GenreCreationDTO[] genresDTO)
        {
            var genres = _mapper.Map<Genre[]>(genresDTO);
            _context.AddRange(genres);
            await _context.SaveChangesAsync();
            return Ok();
        }        
    }
}
