using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [ApiController]
    [Route("api/addresult/configure")]
    public class ConfigureResultsController : ControllerBase
    {
        private readonly StagesService _stagesService;
        private readonly PilotsService _pilotsService;

        public ConfigureResultsController(StagesService stagesService, PilotsService pilotsService)
        {
            _stagesService = stagesService;
            _pilotsService = pilotsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string[] pilots)
        {
            Console.WriteLine(pilots);
            return Ok(new
            {
                pilots = await _pilotsService.GetPilotsToAddResult(pilots.Select(x => Convert.ToInt32(x)).ToList())
            }) ;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody])
    }
}
