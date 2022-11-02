using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class TracksRepository
    {
        private readonly NascarDbContext _context;

        public TracksRepository(NascarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Track>> GetAllAsync()
        {
            return await _context.Tracks.Select(t => t).ToListAsync();
        }
    }
}
