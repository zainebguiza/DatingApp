using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserDto
    {
        public string username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string KnownsAs { get; set; }
        public string Gender { get; set; }
    }
}