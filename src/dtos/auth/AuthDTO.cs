using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catedra_3_Backend.src.dtos
{
    public class AuthDTO
    {
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public List<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = "";

    }
}