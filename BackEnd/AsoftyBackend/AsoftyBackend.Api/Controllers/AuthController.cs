using AsoftyBackend.Infrastructure.Data.DatabaseHandler;
using AsoftyBackend.Application.Model;
using AsoftyBackend.Application.Process;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using AsoftyBackend.Application.Model.Dto;

namespace AsoftyBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return await new LoginHandler().Handle(loginDto, HttpContext);

        }

    }

    
}
