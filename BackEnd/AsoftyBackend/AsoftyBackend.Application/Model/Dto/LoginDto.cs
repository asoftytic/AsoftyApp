using AsoftyBackend.Infrastructure.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoftyBackend.Application.Model.Dto
{
    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int CompanyCode { get; set; }
    }
}
