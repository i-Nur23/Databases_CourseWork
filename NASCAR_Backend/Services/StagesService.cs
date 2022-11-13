using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Services
{
    public class StagesService
    {
        private readonly StagesRepository _stagesRepository;

        public StagesService(StagesRepository repository)
        {
                _stagesRepository = repository;
        }

        public async Task<Stage> GetNearestStageAsync()
        {
            var stage = await _stagesRepository.GetNearestStage();  
            return stage;
        }

        public async Task<IEnumerable<Stage>> GetPastStagesAsync()
        {
            return await _stagesRepository.GetPastStagesAsync();
        }

        public async Task<int> GetNearestStagenumber()
        {
            return (await _stagesRepository.GetNearestStage()).StageNumber;
        }

        public async Task<IEnumerable<Stage>> GetAllStagesAsync()
        {
            return await _stagesRepository.GetAllAsync();
        }
    }
}
