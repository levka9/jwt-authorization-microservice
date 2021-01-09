using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Auth.Models.Requests
{
    public class UpdateUserRequest : CreateUserRequest
    {
        public bool IsActive { get; set; }
    }
}
