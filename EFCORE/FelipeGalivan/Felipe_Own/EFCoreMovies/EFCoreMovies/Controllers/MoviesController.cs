using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTO;
using EFCoreMovies.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genres.OrderByDescending(g=>g.Name).Where(g=>!g.Name.Contains("m")))
                .Include(m => m.CinemaHalls.OrderByDescending(ch=> ch.Cinema.Name))
                    .ThenInclude(ch => ch.Cinema)
                .Include(m=>m.MovieActors)
                    .ThenInclude(ma=>ma.Actor)
                        
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x=> x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("automapper/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithAutoMapper(int id)
        {
            var movie = await _context.Movies
               .ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("lazyloading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetLazyLoading(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id==id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("groupByCinema")]
        public async Task<ActionResult> GetGroupByCinema()
        {
            var groupMovies = await _context.Movies.GroupBy(m => m.InCinemas).Select(g=> new
            {
                InCinemas = g.Key,
                Count = g.Count(),
                Movies = g.ToList()
            }).ToListAsync();

            return Ok(groupMovies);
        }

        [HttpGet("groupByGenresCount")]
        public async Task<ActionResult> GetGroupedByGenresCount()
        {
            var groupedMovies = await _context.Movies.GroupBy(m=>m.Genres.Count()).Select(g=> new
            {
                Count = g.Key,
                Titles = g.Select(m => m.Title),
                Genres = g.Select(m=>m.Genres)
            }).ToListAsync();

            return Ok(groupedMovies);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> Filter([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            var moviesQuaryable = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(movieFilterDTO.Title))
            {
                moviesQuaryable = moviesQuaryable.Where(x=>x.Title.Contains(movieFilterDTO.Title));
            }

            if (movieFilterDTO.InCinemas)
            {
                moviesQuaryable = moviesQuaryable.Where(m => m.InCinemas);
            }

            if (movieFilterDTO.UpcomingReleases)
            {
                var today = DateTime.Today;
                moviesQuaryable = moviesQuaryable.Where(m => m.ReleaseDate > today);
            }

            if (movieFilterDTO.GenreId != 0)
            {
                moviesQuaryable = moviesQuaryable
                    .Where(m => m.Genres.Select(g => g.Id).Contains(movieFilterDTO.GenreId));
            }

            var movies = await moviesQuaryable.Include(m => m.Genres).ToListAsync();
            return _mapper.Map<List<MovieDTO>>(movies);
        }
    }
}
