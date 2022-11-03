using NASCAR_Backend.Repositories;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Services
{
    public class ResultsService
    {
        private readonly ResultsRepository _resultsRepository;
        public ResultsService(ResultsRepository resultsRepository)
        {
            _resultsRepository = resultsRepository;
        }


    }
}
