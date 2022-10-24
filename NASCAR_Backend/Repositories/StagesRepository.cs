using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Models;


namespace NASCAR_Backend.Repositories
{
    public class StagesRepository
    {
        private readonly NascarDbContext _context;

        public StagesRepository(NascarDbContext context)
        {
            _context = context;
        }

        public async Task<Stage> GetNearestStage()
        {
            int nearestStagesID = !_context.Results.Any() ? 1 : _context.Results.Max(u => u.StageID) + 1;

            var stage = await _context.Stages.FirstOrDefaultAsync(u => u.StageNumber == nearestStagesID);
            return stage; 
        }

    }
}
