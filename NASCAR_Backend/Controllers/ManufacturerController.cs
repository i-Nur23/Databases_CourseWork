using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Models;
using NASCAR_Backend.Services;
using System.Text.Json.Serialization;

namespace NASCAR_Backend.Controllers
{
    [Route("api/manufacturer")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly ManufacturerService _manufacturerService;

        public ManufacturerController(ManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new
            {
                manufacturers = await _manufacturerService.GetAllManufacturers()
            }) ;
        }
    }
}
