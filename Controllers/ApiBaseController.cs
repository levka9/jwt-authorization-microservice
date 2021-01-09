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
    }
}
