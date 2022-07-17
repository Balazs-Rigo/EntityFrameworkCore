using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTO;
using EFCoreMovies.Entities;
using EFCoreMovies.Entities.Keyless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCoreMovies.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public CinemasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaDTO>> Get()
        {
            return await _context.Cinemas
                .ProjectTo<CinemaDTO>(mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("closetome")]
        public async Task<ActionResult> Get(double latitude, double longitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var myLocation = geometryFactory
                        .CreatePoint(new Coordinate(longitude, latitude));

            var maxDistanceInMeters = 2000;

            var cinemas = await _context.Cinemas
                .OrderBy(c=>c.Location.Distance(myLocation))
                .Where(c=>c.Location.IsWithinDistance(myLocation,maxDistanceInMeters))
                .Select(c=> new
                {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myLocation))
                }).ToListAsync();

            return Ok(cinemas);
        }

        [HttpPost("withoutLocation")]
        public async Task<IEnumerable<CinemaWithoutLocation>> GetWithoutLocation()
        {
            return await _context.Set<CinemaWithoutLocation>().ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var cinemaLocation = geometryFactory.CreatePoint(new Coordinate(-69.913539, 18.476256));

            var cinema = new Cinema()
            {
                Name = "My cinema",
                Location = cinemaLocation,
                CinemaOffer = new CinemaOffer()
                {
                    DiscountPercentage = 5,
                    Begin = DateTime.Today,
                    End = DateTime.Today.AddDays(7)
                },
                CinemaHalls = new HashSet<CinemaHall>()
                {
                    new CinemaHall()
                    {
                        Cost=200,
                        Currency = Currency.DominicanPeso,
                        CinemaHallType = CinemaHallType.TwoDimensions
                    },
                    new CinemaHall()
                    {
                        Cost=250,
                        Currency = Currency.USDollar,
                        CinemaHallType = CinemaHallType.ThreeDimensions
                    },
                }
            };
            _context.Add(cinema);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("withDTO")]
        public async Task<ActionResult> Post(CinemaCreationDTO cinemaCreationDTO)
        {
            var cinema = mapper.Map<Cinema>(cinemaCreationDTO);
            _context.Add(cinema);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _context.Cinemas.Include(p => p.CinemaHalls).FirstOrDefaultAsync(p => p.Id == id);

            if (cinema is null)
                return NotFound();

            _context.Remove(cinema);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
