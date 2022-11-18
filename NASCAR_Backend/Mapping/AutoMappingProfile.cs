using AutoMapper;
using NASCAR_Backend.Models;
using NASCAR_Backend.Services.ModelsVM;

namespace NASCAR_Backend.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Manufacturer, ManufacturerVM>();
            CreateMap<Team, TeamVM>();
            CreateMap<Result, ResultWithActualNumbersVM>();
            CreateMap<Pilot, PilotResultVM>();
            CreateMap<Manufacturer, ManufacturerResultVM>();
            CreateMap<Team,TeamResultVM>();
        }
    }
}
