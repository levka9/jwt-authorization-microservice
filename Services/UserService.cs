using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JWT.Auth.Helpers;
using System.Threading.Tasks;
using WebApiAuthentication.Services.Interfaces;

namespace JwtWebTokenSerice.Services
{
    public class UserService : IUserService
    {
        //IUserRepository repository;

        //public UserService(IUserRepository Repository)
        //{
        //    repository = Repository;
        //}

        //public async Task<User> Authenticate(string Identity)
        //{
        //    var user = await repository.GetUserIncludeRoleAsync(Identity);

        //    return user;
        //}

        //public async Task<User> Authenticate(string Username, string Password)
        //{
        //    var user = await repository.GetUserIncludeRoleAsync(Username, Password);

        //    return user;
        //}        
    }
}