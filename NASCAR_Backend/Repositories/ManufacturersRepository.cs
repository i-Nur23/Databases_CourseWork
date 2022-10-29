using NASCAR_Backend.Context;

namespace NASCAR_Backend.Repositories
{
    public class ManufacturersRepository
    {
        private readonly NascarDbContext _context;

        public ManufacturersRepository(NascarDbContext context)
        {
            _context = context;
        }
    }
}
