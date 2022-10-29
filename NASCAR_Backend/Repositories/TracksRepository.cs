using NASCAR_Backend.Context;

namespace NASCAR_Backend.Repositories
{
    public class TracksRepository
    {
        private readonly NascarDbContext _context;

        public TracksRepository(NascarDbContext context)
        {
            _context = context;
        }
    }
}
