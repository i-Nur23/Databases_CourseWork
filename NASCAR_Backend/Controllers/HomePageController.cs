using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Models;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomePageController : ControllerBase
    {
        private readonly PilotsService _pilotsService;

        public HomePageController(PilotsService pilotsService)
        {
            _pilotsService = pilotsService;               
        }

        [HttpGet(Name = "GetTop5Pilots")]
        public async  Task<IEnumerable<PilotInfo>> GetTopFive()
        {
            var pilots = await _pilotsService.GetTopFivePilotsAsync();
            var result = new List<PilotInfo>();
            foreach (var item in pilots)
            {
                result.Add(new PilotInfo(item.Id, item.Name, item.SurName, item.CarsNumber, item.Points, item?.Team?.Name));
            }
            return result;
        }

        public record class PilotInfo (int Id, string Name, string Surname,int Number ,int Points, string? Team);
    }
}