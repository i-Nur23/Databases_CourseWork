using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class PilotsRepository
    {
        private readonly NascarDbContext _context;

        public PilotsRepository(NascarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pilot>> GetParticipatingPilots()
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;
            return await _context.Pilots
                .Where(p => p.CarsNumber != 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pilot>> GetPilotsByOrder()
        {
            return await _context.Pilots
                .OrderBy(p => p.Points)
                .ToListAsync();
        }
    }
}
