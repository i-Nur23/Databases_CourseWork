using NASCAR_Backend.Context;

namespace NASCAR_Backend.Repositories
{
    public class ChangesRepository
    {
        private readonly NascarDbContext _context;

        public ChangesRepository(NascarDbContext context)
        {
            _context = context;
        }
    }
}
