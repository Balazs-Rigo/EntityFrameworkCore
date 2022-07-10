using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMovies.DTO;
using EFCoreMovies.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get(int page = 1, int recordsToTake = 2)
        {
            return await _context.Actors.AsNoTracking()
                .OrderBy(x => x.Name)
                .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider)
                .Paginate(page, recordsToTake)
                .ToListAsync();
        }

        [HttpGet("ids")]
        public async Task<IEnumerable<int>> GetIds()
        {
            return await _context.Actors.Select(a =>a.Id).ToListAsync();
        }
        
    }
}
