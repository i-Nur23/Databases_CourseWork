using Microsoft.EntityFrameworkCore;
using NASCAR_Backend.Context;
using NASCAR_Backend.Models;

namespace NASCAR_Backend.Repositories
{
    public class ManufacturersRepository
    {
        private readonly NascarDbContext _context;

        public ManufacturersRepository(NascarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manufacturer>> GetAll()
        {
            return await _context.Manufacturers.ToListAsync();
        }
    }
}
