using JWT_Auth.Microservice.Entities;
using JWT.Auth.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_Auth.Microservice.Modules.Interafaces
{
    public interface IJwtTokenModule
    {
        Task<string> Create(UserTokenRequest UserParam);
        Task<string> GetToken(UserTokenRequest UserParam);
        Task<bool> IsTokenValid(string TokenKey);
    }
}
