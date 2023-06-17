using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Blazor_WebAssembly_Login_1.Shared;

namespace Blazor_WebAssembly_Login_1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Create the Login method to verify the login and validate if it is the admin by priority
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            SessionDTO sessionDTO = new SessionDTO();

            if (login.Email == "admin@gmail.com" && login.Password == "admin")
            {
                sessionDTO.Name = "Admin";
                sessionDTO.Email = login.Email;
                sessionDTO.Rol = "Admin";
            }
            else
            {
                sessionDTO.Name = "Employee";
                sessionDTO.Email = login.Email;
                sessionDTO.Rol = "Employee";
            }

            return StatusCode(StatusCodes.Status200OK, sessionDTO);

        }
    }
}
