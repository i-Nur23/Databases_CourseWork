using NASCAR_Backend.Models;
using NASCAR_Backend.Repositories;
using AutoMapper;
using NASCAR_Backend.Services.ModelsVM;

namespace NASCAR_Backend.Services
{
    public class TeamsService
    {
        private readonly TeamsRepository _teamsRepository;
        private readonly PilotsRepository _pilotsRepository;
        private readonly IMapper _mapper;

        public TeamsService(TeamsRepository teamsRepository, PilotsRepository pilotsRepository, IMapper mapper)
        {
            _teamsRepository = teamsRepository;
            _pilotsRepository = pilotsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _teamsRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TeamVM>> GetAllWithPoints()
        {
            var teams = await _teamsRepository.GetAllAsync();
            var result = new List<TeamVM>();
            foreach (var team in teams)
            {
                var teamWithPoints = _mapper.Map<TeamVM>(team);
                teamWithPoints.Points = await _pilotsRepository.GetTeamsPoints(team);
                result.Add(teamWithPoints);
            }

            return result;
        }

    }
}
