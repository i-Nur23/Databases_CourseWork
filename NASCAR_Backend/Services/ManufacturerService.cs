using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Models;
using NASCAR_Backend.Repositories;

namespace NASCAR_Backend.Services
{
    public class ManufacturerService
    {
        private readonly ManufacturersRepository _manufacturersRepository;
        private readonly PilotsRepository _pilotsRepository;

        public ManufacturerService(ManufacturersRepository manufacturersRepository, PilotsRepository pilotsRepository)
        {
            _manufacturersRepository = manufacturersRepository;
            _pilotsRepository = _pilotsRepository;
        }

        public async Task<IEnumerable<(Manufacturer, int)>> GetAllManufacturers()
        {
            var manufacturers = await _manufacturersRepository.GetAll();
            var result = new List<(Manufacturer, int)>();
            foreach (var manuf in manufacturers)
            {
                var points = await _pilotsRepository.GetmanufacturerPoints(manuf);
                result.Add((manuf, points));
            }

            return result;
        }

    }
}
