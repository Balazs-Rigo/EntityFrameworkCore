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

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get(int page = 1, int recordsToTake = 2)
        {
            return await _context.Actors.AsNoTracking()
                .OrderBy(x => x.Name)
                .Select(a => new ActorDTO { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth })
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
