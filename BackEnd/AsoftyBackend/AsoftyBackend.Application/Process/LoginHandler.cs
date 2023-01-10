using AsoftyBackend.Application.Model;
using AsoftyBackend.Application.Model.Dto;
using AsoftyBackend.Infrastructure.Data.DatabaseHandler;
using AsoftyBackend.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoftyBackend.Application.Process
{
    public class LoginHandler 
    {
        public async Task<IActionResult> Handle(LoginDto loginDto, HttpContext context)
        {

            try
            {
                //var context = HttpContext;
                var db = new QueryGenericHandler<User>();

                //  Query Example
                var user = await db.Where(new
                {
                    CompanyCode = loginDto.CompanyCode,
                    Username = loginDto.Username,
                    Password = loginDto.Password,
                    Enabled = true
                })
                .QueryAsync();


                return HttpUtils.ReturnData(user);


            }
            catch (Exception ex)
            {
                return HttpUtils.ReturnInternalServerError(ex.Message);
            }

        }
    }
}
