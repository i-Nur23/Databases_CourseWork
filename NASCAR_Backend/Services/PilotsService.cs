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


    }
}
