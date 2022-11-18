using Microsoft.EntityFrameworkCore;
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



        /*public async Task<int> GetWinsInThisRound (int currentStageNumber, Pilot pilot)
        {
            switch (true)
            {
                case true when currentStageNumber < 26:
                    return pilot.Wins;

                case true when currentStageNumber >= 26 && currentStageNumber < 29:
                    return winsAtTop16[pilot];

                case true when currentStageNumber >= 29 && currentStageNumber < 32:
                    return winsAtTop12[pilot];

                case true when currentStageNumber >= 32 && currentStageNumber < 35:
                    return winsAtTop8[pilot];

                default:
                    return 0;
            }
        }*/

        public async Task<int> GetNumberOfCurrentStageAsync()
        {
            return _context.Results.Max(u => u.StageID);
        }

        public async Task<int> GetCurrentRoundsCount()
        {
            int currentStageNumber = await GetNumberOfCurrentStageAsync();
            switch (true)
            {
                case true when currentStageNumber >= 26 && currentStageNumber < 29:
                    return 16;

                case true when currentStageNumber >= 29 && currentStageNumber < 32:
                    return 12;

                case true when currentStageNumber >= 32 && currentStageNumber < 35:
                    return 8;

                case true when currentStageNumber == 36:
                    return 4;

                default:
                    return 0;
            }
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

        public async Task AddResult(List<Result> results)
        {
            await Task.Run(async () =>
            {
                await _context.Results.AddRangeAsync(results);
                _context.SaveChanges();
            });
        }

        public async Task<IEnumerable<Result>> GetByStageID(int stageId)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            return _context.Results
                .Where(r => r.StageID == stageId)
                .OrderBy(r => r.Place)
                .Include(r => r.Pilot)
                .Include(r => r.Stage)
                .Include(r => r.Pilot.Team);
        }

        /*public async Task<int[,]> GetPilotsTable()
        {
            var pilotsCount = await _context.Pilots.CountAsync();
            const int stagesCount = 36;

            var arrayOfResults = new int[pilotsCount, stagesCount];

            foreach (var res in _context.Results)
            {
                arrayOfResults[ await PilotPlace(res.PilotID), res.StageID] = res.Place;
            }

            return arrayOfResults;
        }*/

        /*public async Task<int> PilotPlace(int id)
        {
            var currentStageNum = await GetNumberOfCurrentStageAsync();

            var pilotToFind = await _context.Pilots.FirstOrDefaultAsync(x => x.Id == id);

            var currentRound = await GetCurrentRoundsCount(currentStageNum);

            if (pilotToFind.PlayOffStatus)
            {
                return _context.Pilots
                .OrderByDescending(x => x.Wins)
                .ThenByDescending(x => x.Points)
                .ToList()
                .IndexOf(pilotToFind);
            }
        }*/

        public async Task<IEnumerable<Result>> GetAllResults()
        {
            return await _context.Results
                .OrderBy(r => r.StageID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetPilotsResults(int id)
        {
            return await _context.Results
                .Where(r => r.PilotID == id)
                .OrderBy(r => r.StageID)
                .ToListAsync();
        }


    }
}
