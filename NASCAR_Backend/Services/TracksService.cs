using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;
namespace NASCAR_Backend.Services
{
    public class TracksService
    {
        private readonly TracksRepository _trackRepository;

        public TracksService(TracksRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<IEnumerable<Track>> GetTracksAsync()
        {
            return await _trackRepository.GetAllAsync();
        }
    }
}
