using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Services
{
    public class StagesService
    {
        private readonly StagesRepository _repository;

        public StagesService(StagesRepository repository)
        {
                _repository = repository;
        }

        public async Task<Stage> GetNearestStageAsync()
        {
            var stage = await _repository.GetNearestStage();  
            return stage;
        }
    }
}
