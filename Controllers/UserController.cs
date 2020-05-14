using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JWT.Auth.Entities;
using JWT.Auth.Models.Requests;
using JWT.Auth.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Auth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IMapper mapper;
        JWTAuthContext context;

        public UserController(JWTAuthContext Context, IMapper Mapper)
        {
            this.mapper = Mapper;
            this.context = Context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("test");
        }

        [HttpPut]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest User)
        {
            var userModule = new UserModule(context, mapper);
            var response = await userModule.Add(User);

            return Ok(response);
        }
    }
}