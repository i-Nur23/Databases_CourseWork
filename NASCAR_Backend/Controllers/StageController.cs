using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [Route("api/stages")]
    public class StageController : ControllerBase
    {
        private readonly StagesService _stagesService;

        public StageController(StagesService stagesService)
        {
            _stagesService = stagesService;
        }

        [HttpGet("getpast")]
        public async Task<IActionResult> GetPast()
        {
            return Ok(new
            {
                stages = await _stagesService.GetPastStagesAsync()
            }); ; ;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new
            {
                stages = await _stagesService.GetAllStagesAsync(),
                nearestStage = await _stagesService.GetNearestStagenumber()
            });
        }

    }
}
