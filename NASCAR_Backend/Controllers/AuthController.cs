using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;

namespace NASCAR_Backend.Controllers
{
    public class AuthController : ControllerBase
    {
        private List<string> passwords = new  List<string> { "12345", "pass_word_97", "m4st3r" };
        private readonly JwtService _jwtService;


        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }


        [HttpPost]
        [Route("auth")]
        public IActionResult Authentificate(string password)
        {
            if (!passwords.Contains(password))
            {
                return Ok(new {
                    message = "wrong"
                }); 
            }

            var jwt = _jwtService.Generate();

            Response.Cookies.Append("jwt", jwt);

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
