using JWT_Auth.Microservice.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWT.Auth.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected long? GetUserIdFromToken()
        {
            long? result;

            var identifier = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (identifier == null) 
                result = null;
            else 
                result = long.Parse(identifier.Value);

            return result;
        }

        protected IActionResult GetResponse(object Object, string Message = "success", int Status = 200)
        {
#pragma warning disable CS0436 // Type conflicts with imported type
            return Ok(new BaseHttpResponse()
#pragma warning restore CS0436 // Type conflicts with imported type
            { 
                Object = Object, 
                Message = Message, 
                Status = Status 
            });
        }
    }
}
