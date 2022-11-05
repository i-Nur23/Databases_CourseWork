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
        public IActionResult GetByStage(int stageId)
        {
            return Ok(new
            {
                results = _resultsService.GetByStageID(stageId)
            });
        }
    }
}
