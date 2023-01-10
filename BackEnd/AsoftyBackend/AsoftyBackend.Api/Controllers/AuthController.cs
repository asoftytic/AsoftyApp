using AsoftyBackend.Infrastructure.Data.DatabaseHandler;
using AsoftyBackend.Infrastructure.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AsoftyBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //  TODO: Refactorizar para cumplir estándares de comunicación
            try
            {
                
                var db = new QueryGenericHandler<User>();

                var companyCode = loginDto.CompanyCode;
                var Username = loginDto.Username;
                var Password = loginDto.Password;

                //  Query Example
                //var user1 = await db.Where(u => 
                //    u.CompanyCode == companyCode && 
                //    u.Username == Username && 
                //    u.Password == Password).QueryAsync();

                //return Ok(user1);

                return Ok(new { });

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int CompanyCode { get; set; }
    }
}
