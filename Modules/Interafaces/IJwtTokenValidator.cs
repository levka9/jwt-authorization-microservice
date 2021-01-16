using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Auth.Microservice.Modules.Interafaces
{
    public interface IJwtTokenValidator
    {
        JWT_Auth.Microservice.Entities.Token Token { get; }
        Task<bool> IsTokenValid(string TokenKey);
    }
}
