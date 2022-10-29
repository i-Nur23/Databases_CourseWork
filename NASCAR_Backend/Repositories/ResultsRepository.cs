using NASCAR_Backend.Context;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class ResultsRepository
    {
        private readonly NascarDbContext _context;

        public ResultsRepository(NascarDbContext context)
        {
            _context = context;
        }


        public async Task<int> GetNumberOfCurrentStageAsync()
        {
            return _context.Results.Max(u => u.StageID);
        }

        public async Task<int> GetNumberOfNearestStageAsync ()
        {
            var results = _context.Results;
            if (!results.Any())
            {
                return 1;
            }

            return await GetNumberOfCurrentStageAsync() + 1;
        }
    }
}
