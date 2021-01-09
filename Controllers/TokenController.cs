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

namespace JwtWebTokenSerice.Controllers
{        
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        #region Properties
        JWTAuthContext context;
        IOptions<AppSettings> appSettings;
        #endregion

        #region Constructors
        public TokenController(JWTAuthContext Context,
                               IOptions<AppSettings> AppSettings)
        {
            context = Context;
            appSettings = AppSettings;
        }
        #endregion

        //[EnableCors("CorsPolicy")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]UserTokenRequest UserParam)
        {
            var browserCapabilities = HttpContext.Request.Headers["User-Agent"].ToString();
            var ipAdderess = HttpContext.Connection.RemoteIpAddress;
            var hostUrl = HttpContext.Request.Host.ToString();

            var user = await context.User.Include(x => x.UserUserRole)
                                         .ThenInclude(x => x.UserRole)
                                         .FirstOrDefaultAsync(x => x.Username == UserParam.Username &&
                                                                   x.Password == UserParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var userRoles = user.UserUserRole.Select(x => x.UserRole.Name);

            var jwtTokenModule = new JwtTokenModule(user.Id, userRoles, context, appSettings);
            var token = await jwtTokenModule.GetToken(browserCapabilities, ipAdderess.ToString(), hostUrl, UserParam.IsGenerateNewToken);

            return Ok(new { token_type = "Bearer", access_token = token });
        }

        [HttpGet]
        [ActionName("is-valid")]
        public async Task<IActionResult> IsTokenValid([FromQuery]string Token)
        {
            IJwtTokenValidator jwtTokenValidator = new JwtTokenModule(Token, context);
            var result = await jwtTokenValidator.IsTokenValid();

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
