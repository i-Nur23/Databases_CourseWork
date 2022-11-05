using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Models;


namespace NASCAR_Backend.Repositories
{
    public class StagesRepository
    {
        private readonly NascarDbContext _context;
        private readonly ResultsRepository _resultsRepository;

        public StagesRepository(NascarDbContext context, ResultsRepository results)
        {
            _context = context;
            _resultsRepository = results;
        }

        public async Task<Stage> GetNearestStage()
        {
            int nearestStagesID = await _resultsRepository.GetNumberOfNearestStageAsync();

            var stage = await _context.Stages.FirstOrDefaultAsync(u => u.StageNumber == nearestStagesID);
            return stage; 
        }

        public async Task<IEnumerable<Stage>> GetPastStagesAsync()
        {
            int nearestStagesID = await _resultsRepository.GetNumberOfNearestStageAsync();

            return await _context.Stages.Where(x => x.StageNumber < nearestStagesID).ToListAsync();
        }

    }
}
