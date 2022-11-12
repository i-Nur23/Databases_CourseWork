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
        private readonly TeamsService _teamsService;

        public PilotController(PilotsService pilotsService, TeamsService teamsService)
        {
            _pilotService = pilotsService;
            _teamsService = teamsService;
        }
        

        [HttpPost]
        public async Task<IActionResult> PostPilot([FromBody] PilotToUpdate pilot)
        {
            Console.WriteLine(pilot);
            await _pilotService.AddPilotAsync(pilot);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPilots()
        {
            return Ok(new
            {
                pilots = await _pilotService.GetPilotsAsync()
            }) ; ;
        }

        [HttpGet("change/{id}")]
        public async Task<IActionResult> GetPilotById(int id)
        {
            try
            {
                return Ok(new
                {
                    pilot = await _pilotService.GetByIdAsync(id),
                    teams = await _teamsService.GetAllAsync()
                }); ;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("change/{id}")]
        public async Task<IActionResult> ChangePilotsInfo([FromBody] PilotToUpdate pilot, [FromRoute]int id)
        {
            try
            {
                await _pilotService.PutPilot(id, pilot);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("nums")]
        public async Task<IActionResult> GetPilotsWithNums()
        {
            var pilotsWithNums = (await _pilotService.GetPilotsAsync()).Select(x => new {id = x.Id, name = x.Name, surName = x.SurName, carsNumber = x.CarsNumber });
            return Ok(new
            {
                pilots = pilotsWithNums
            }); ;
        }  

        [HttpPost("nums")]
        public async Task<IActionResult> ChangePilotsNums([FromBody] PilotWithNewNumber pilot)
        {
            await _pilotService.ChangePilotsNumberAsync(pilot.Id, pilot.Number);
            return Ok();
        }

    }
}
