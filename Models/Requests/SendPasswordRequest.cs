using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Auth.Microservice.Models.Requests
{
    public class SendPasswordRequest
    {
        public long ApplicationId { get; set; }
        public string Email { get; set; }        
    }
}
