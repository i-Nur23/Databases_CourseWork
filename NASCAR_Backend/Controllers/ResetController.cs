using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers;

[Route("api/reset")]
[ApiController]
public class ResetController : ControllerBase
{
    private readonly PilotsService _pilotsService;
    private readonly ResultsService _resultsService;
    private readonly ChangesService _changesService;

    public ResetController(PilotsService pilotsService, ResultsService resultsService, ChangesService changesService)
    {
        _changesService = changesService;
        _pilotsService = pilotsService;
        _resultsService = resultsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new
        {
            pilots = await _pilotsService.GetPilotBriefInfo()
        });
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int[] ids)
    {
        _changesService.DeleteAll();
        _resultsService.DeleteAll(ids.ToList());
        return Ok();
    }
}