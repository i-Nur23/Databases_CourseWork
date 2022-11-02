using Microsoft.AspNetCore.Mvc;
using NASCAR_Backend.Services;
using System.Web;

namespace NASCAR_Backend.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private List<string> passwords = new  List<string> { "12345", "pass_word_97", "m4st3r" };
        private readonly JwtService _jwtService;


        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult GetToken()
        { 
            return Ok(new
            {
                token = Request.Cookies["jwt"]
            }); 
        }

        [HttpDelete]
        public IActionResult DeleteToken()
        {
            CookieOptions options = new CookieOptions();

            options.Expires = DateTime.Now.AddDays(-1d);

            Response.Cookies.Append("jwt","");

            return Ok();
        }


        [HttpPost]
        public IActionResult Authentificate([FromBody]string password)
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
