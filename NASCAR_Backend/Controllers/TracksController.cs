using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    [ApiController]
    [Route("api/tracks")]
    public class TracksController : ControllerBase
    {
        private readonly TracksService _tracksService;

        public TracksController(TracksService tracksService)
        {
            _tracksService = tracksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new
            {
                tracks = await _tracksService.GetTracksAsync()
            }) ; ;
        }
    }
}