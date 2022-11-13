using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [Route("api/team")]
    public class TeamsController : ControllerBase
    {
        private readonly TeamsService _teamsService;
        public TeamsController( TeamsService teamsService)
        {
            _teamsService = teamsService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new
            {
                teams = await _teamsService.GetAllAsync()
            }); ;
        }

        [HttpGet("allwithpoints")]
        public async Task<IActionResult> GetAllWithPoints()
        {
            return Ok(new
            {
                teams = await _teamsService.GetAllWithPoints()
            });
        }    
    }
}
