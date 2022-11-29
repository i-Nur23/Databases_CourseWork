using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Controllers.Jsons;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ResultsService _resultsService;
        private readonly PilotsService _pilotsService;
        private readonly StagesService _stagesService;
        
        public ResultsController(ResultsService resultsService, PilotsService pilotsService,
            StagesService stagesService)
        {
            _resultsService = resultsService;
            _pilotsService = pilotsService;
            _stagesService = stagesService;
        }

        [HttpGet("byStage/{stageId}")]
        public async Task<IActionResult> GetByStage(int stageId)
        {
            return Ok(new
            {
                results = await _resultsService.GetByStageID(stageId)
            });
        }

        [HttpGet("withNums/{stageId}")]
        public async Task<IActionResult> GetByStageWithNums(int stageId)
        {
            return Ok(new
            {
                results = await _resultsService.GetByStageIDWithActualNumbers(stageId)
            }) ;
        }

        [HttpGet("table")]
        public async Task<IActionResult> GetTable()
        {
            return Ok(new
            {
                pilotsRes = await _resultsService.GetPilotsTable(),
                teamsRes = await _resultsService.GetTeamsTable(),
                manRes = await _resultsService.GetManufacturersTable(),
                currentRound = await _resultsService.CurrentRound()
            }) ;
        }
        
        [HttpGet("add/show")]
        public async Task<IActionResult> GetAllPilots()
        {
            return Ok(new
            {
                stage = await _stagesService.GetNearestStageAsync(),
                pilots = await _pilotsService.GetParticipatingPilots()
            }) ;
        }
        
        [HttpGet("configure/show")]
        public async Task<IActionResult> GetChoosenPilots([FromQuery] string[] pilots)
        {
            Console.WriteLine(pilots);
            return Ok(new
            {
                pilots = await _pilotsService.GetPilotsToAddResult(pilots.Select(x => Convert.ToInt32(x)).ToList())
            }) ;
        }
        
        [HttpPost("configure")]
        public async Task<IActionResult> Post([FromBody] PlaceJson.PlaceInfo[] Res) {
            await _resultsService.AddResultsAsync(Res);

            return Ok();
        }
        
        
    }
}
