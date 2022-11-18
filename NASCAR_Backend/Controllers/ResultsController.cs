using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ResultsService _resultsService;
        public ResultsController(ResultsService resultsService)
        {
            _resultsService = resultsService;
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
    }
}
