using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class PilotsRepository
    {
        private readonly NascarDbContext _context;
        private readonly Dictionary<int, int> placeToPoints;


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

        public async Task SetPilotspoints(List<Result> results)
        {
            await Task.Run(() =>
            {
                if (results[0].StageID != 26 || results[0].StageID != 29 || results[0].StageID != 32 || results[0].StageID != 35)
                {
                    foreach (var item in results)
                    {
                        int points;
                        if (item.Place == 1)
                        {
                            points = 40;
                        }
                        else
                        {
                            points = 35 - (item.Place - 2);
                            if (points <= 0) { points = 1; } 
                        }
                        var pilot = _context.Pilots.FirstOrDefault(x => x.Id == item.PilotID);
                        pilot.Points += points;
                        _context.Pilots.Update(pilot);
                        _context.SaveChanges();
                    }
                }
            });
        }


    }
}
