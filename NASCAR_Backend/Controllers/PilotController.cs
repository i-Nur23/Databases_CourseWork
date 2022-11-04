using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Controllers.Jsons;
using NASCAR_Backend.Services;
using System.Web;

namespace NASCAR_Backend.Controllers
{
    [Route("api/pilot")]
    public class PilotController : ControllerBase
    {
        private readonly PilotsService _pilotService;

        public PilotController(PilotsService pilotsService)
        {
            _pilotService = pilotsService;
        }
        

        [HttpPost]
        public async Task<IActionResult> PostPilot([FromBody] PilotToUpdate pilot)
        {
            await _pilotService.AddPilotAsync(pilot);
            return Ok();
        }
    }
}
