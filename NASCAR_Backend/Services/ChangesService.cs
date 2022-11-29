using NASCAR_Backend.Repositories;

namespace NASCAR_Backend.Services
{
    public class ChangesService
    {
        private readonly ChangesRepository _repository;

        public ChangesService(ChangesRepository repository)
        {
            _repository = repository;
        }

        public void DeleteAll()
        {
            _repository.DeleteAll();
        }

    }
}
