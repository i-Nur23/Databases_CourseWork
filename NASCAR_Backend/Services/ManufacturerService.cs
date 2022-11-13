using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Models;
using NASCAR_Backend.Repositories;
using NASCAR_Backend.Services.ModelsVM;

namespace NASCAR_Backend.Services
{
    public class ManufacturerService
    {
        private readonly ManufacturersRepository _manufacturersRepository;
        private readonly PilotsRepository _pilotsRepository;
        private readonly IMapper _mapper;

        public ManufacturerService(ManufacturersRepository manufacturersRepository, PilotsRepository pilotsRepository, IMapper mapper)
        {
            _manufacturersRepository = manufacturersRepository;
            _pilotsRepository = pilotsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManufacturerVM>> GetAllManufacturers()
        {
            var manufacturers = await _manufacturersRepository.GetAll();
            var result = new List<ManufacturerVM>();
            foreach (var manuf in manufacturers)
            {
                var manufWithPoints = _mapper.Map<ManufacturerVM>(manuf);
                manufWithPoints.Points = await _pilotsRepository.GetmanufacturerPoints(manuf);
                result.Add(manufWithPoints);
            }

            return result;
        }

    }
}
