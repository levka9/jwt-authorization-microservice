using Microsoft.AspNetCore.Mvc;
using JwtWebTokenSerice.Modules;
using Microsoft.Extensions.Options;
using JWT.Auth.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JWT.Auth.Models.Requests;
using System.Linq;
using JWT.Auth.Modules.Interafaces;
using Microsoft.AspNetCore.Cors;
using JWT.Auth.Entities.Context;
using JWT_Auth.Microservice.Modules.Interafaces;

namespace JwtWebTokenSerice.Controllers
{        
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region Properties
        IJwtTokenModule jwtTokenModule;
        IJwtTokenValidator jwtTokenValidator;
        #endregion

        #region Constructors
        public TokenController(IJwtTokenModule JwtTokenModule,
                               IJwtTokenValidator JwtTokenValidator)
        {
            jwtTokenModule = JwtTokenModule;
            jwtTokenValidator = JwtTokenValidator;
        }
        #endregion

        //[EnableCors("CorsPolicy")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]UserTokenRequest UserParam)
        {
            UserParam.BrowserCapabilities = HttpContext.Request.Headers["User-Agent"].ToString();
            UserParam.IpAdderess = HttpContext.Connection.RemoteIpAddress.ToString();
            UserParam.HostUrl = HttpContext.Request.Host.ToString();

            var token = await jwtTokenModule.GetToken(UserParam);

            return Ok(new { token_type = "Bearer", access_token = token });
        }

        [HttpGet]
        [ActionName("is-valid")]
        public async Task<IActionResult> IsTokenValid([FromQuery]string Token)
        {
            var result = await jwtTokenValidator.IsTokenValid(Token);

            if (result)
            {
                return Ok(new { expirationDate = jwtTokenValidator.Token.ExpirationDate.ToString() }); 
            }
            else
            {
                return BadRequest(new { message = "Token not exists" });
            }
        }
    }
}
