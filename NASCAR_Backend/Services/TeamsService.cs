using NASCAR_Backend.Models;
using NASCAR_Backend.Repositories;

namespace NASCAR_Backend.Services
{
    public class TeamsService
    {
        private readonly TeamsRepository _teamsRepository;

        public TeamsService(TeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _teamsRepository.GetAllAsync();
        }

    }
}
