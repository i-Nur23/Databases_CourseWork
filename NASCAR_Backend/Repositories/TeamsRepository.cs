using NASCAR_Backend.Context;

namespace NASCAR_Backend.Repositories
{
    public class TeamsRepository
    {
        private readonly NascarDbContext _context;

        public TeamsRepository(NascarDbContext context)
        {
            _context = context;
        }

    }
}
