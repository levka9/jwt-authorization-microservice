using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Models.Requests
{
    public class UserTokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsGenerateNewToken { get; set; }
    }
}
