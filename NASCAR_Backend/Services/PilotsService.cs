using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Services
{
    public class PilotsService
    {
        private readonly PilotsRepository _repository;

        public PilotsService(PilotsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Pilot>> GetParticipatingPilots()
        {
            return await _repository.GetParticipatingPilots();
        }
        public async Task<IEnumerable<Pilot>> GetPilotsToAddResult(List<int> IDs)
        {
            var pilots = await _repository.GetParticipatingPilots();
            return pilots.Where(x => IDs.Contains(x.Id));
        }

        public async Task<IEnumerable<Pilot>> GetPilotsAsync()
        {
            var pilots = await _repository.GetPilotsByOrder();
            return pilots;
        }

        public async Task<IEnumerable<Pilot>> GetTopFivePilotsAsync()
        {
            var pilots = await _repository.GetPilotsByOrder();
            return pilots.Take(5);
        }

        public async Task AddPilotAsync(Pilot pilot)
        {
            await Task.Run(async () => {
                await _repository.AddPilotAsync(pilot);
            });
        }
    }
}
