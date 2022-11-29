using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Controllers.Jsons;
using NASCAR_Backend.Models;
using NASCAR_Backend.Services;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NASCAR_Backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomePageController : ControllerBase
    {
        private readonly PilotsService _pilotsService;
        private readonly StagesService _stagesService;

        public HomePageController(PilotsService pilotsService, StagesService stagesService)
        {
            _pilotsService = pilotsService;
            _stagesService = stagesService;
        }

        [HttpGet]
        public async Task<HomePageJson> GetTopFive()
        {
            var pilots = await _pilotsService.GetTopFivePilotsAsync();
            var result = new List<PilotInfo>();
            foreach (var item in pilots)
            {
                result.Add(new PilotInfo(item.Id, item.Name, item.SurName, item.Wins ,item.CarsNumber, item.Points, item?.Team?.Name));
            }

            var nearestStage = await _stagesService.GetNearestStageAsync();
            NearestStageInfo stage;
            if (nearestStage == null)
            {
                stage = null;
            }
            else
            {
                stage = new NearestStageInfo(nearestStage.Name, nearestStage.EventsDate, nearestStage.Track.Name);    
            }

            var json = new HomePageJson
            {
                Pilots = result,
                NearestStage = stage,
            };

            return json;

        }
    }
}