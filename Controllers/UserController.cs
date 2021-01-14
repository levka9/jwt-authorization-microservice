using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Auth.Entities;
using JWT.Auth.Entities.Context;
using JWT.Auth.Models.Requests;
using JWT.Auth.Modules;
using JWT.Auth.Modules.Interafaces;
using JWT_Auth.Microservice.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Auth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        IUserModule userModule;

        public UserController(IUserModule UserModule)
        {
            userModule = UserModule;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            long? userId = GetUserIdFromToken();

            var user = await userModule.Get(userId);

            return Ok(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetByCredentials([FromQuery]UserGetByCredentialsRequest Request)
        {
            var user = await userModule.GetByCredentials(Request);

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotal()
        {
            var count = await userModule.Quantity();

            return Ok(new { TotalUsers = count });
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Web server running.");
        }

        [HttpPut]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest User)
        {
            var response = await userModule.AddAsync(User);

            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update([FromBody]UpdateUserRequest Request)
        {
            var response = await userModule.Update(Request);

            return null;
        }
    }
}