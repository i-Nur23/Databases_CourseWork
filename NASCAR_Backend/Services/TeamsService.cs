using NASCAR_Backend.Models;
using NASCAR_Backend.Repositories;

namespace NASCAR_Backend.Services
{
    public class TeamsService
    {
        private readonly TeamsRepository _teamsRepository;
        private readonly PilotsRepository _pilotsRepository;

        public TeamsService(TeamsRepository teamsRepository, PilotsRepository pilotsRepository)
        {
            _teamsRepository = teamsRepository;
            _pilotsRepository = pilotsRepository;  
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _teamsRepository.GetAllAsync();
        }

        public async Task<IEnumerable<(Team, int)>> GetAllWithPoints()
        {
            var teams = await _teamsRepository.GetAllAsync();
            var result = new List<(Team, int)>();
            foreach (var team in teams)
            {
                var points = await _pilotsRepository.GetTeamsPoints(team);
                result.Add((team, points));
            }

            return result;
        }

    }
}
