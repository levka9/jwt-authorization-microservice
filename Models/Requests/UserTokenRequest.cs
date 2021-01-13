using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Models.Requests
{
    public class UserTokenRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsGenerateNewToken { get; set; }
        public string BrowserCapabilities { get; set; }
        public string IpAdderess { get; set; }
        public string HostUrl { get; set; }
    }
}
