using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [ApiController]
    [Route("api/addresult")]
    public class AddResultsController : ControllerBase
    {
        private readonly StagesService _stagesService;
        private readonly PilotsService _pilotsService;

        public AddResultsController(StagesService stagesService, PilotsService pilotsService)
        {
            _stagesService = stagesService;
            _pilotsService = pilotsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new
            {
                stage = await _stagesService.GetNearestStageAsync(),
                pilots = await _pilotsService.GetParticipatingPilots()
            }) ;
        }
    }
}
