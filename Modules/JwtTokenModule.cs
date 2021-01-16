using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWT_Auth.Microservice.Entities;
using JWT_Auth.Microservice.Entities.Context;
using JWT.Auth.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using JWT.Auth.Models.Enums;
using JWT.Auth.Modules.Interafaces;
using Newtonsoft.Json;
using JWT_Auth.Microservice.Modules.Interafaces;
using JWT.Auth.Models.Requests;
using Microsoft.Extensions.Configuration;
using JWT_Auth.Microservice.Models.Requests;

namespace JwtWebTokenSerice.Modules
{
    public class JwtTokenModule : IJwtTokenModule, IJwtTokenValidator
    {
        #region Properties
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        IConfiguration configuration;
        IEnumerable<string> lstUserRoles;        
        JWT_Auth.Microservice.Entities.Token token;

        User user;
        IUserModule userModule;
        JWTAuthContext context;

        public JWT_Auth.Microservice.Entities.Token Token 
        {
            get { return token; }
        }
        #endregion

        #region Constructors
        public JwtTokenModule(IUserModule UserModule, JWTAuthContext Context, IConfiguration Configuration)
        {
            userModule = UserModule;
            context = Context;
            configuration = Configuration;
        }
        #endregion


        #region Public Methods
        public async Task<string> GetToken(UserTokenRequest UserParam)
        {
            
            user = await userModule.GetByCredentials(new UserGetByCredentialsRequest() 
                                                    { 
                                                        Password = UserParam.Password, 
                                                        Username = UserParam.Username
                                                    });

            if (user == null)
                throw new Exception("Username or password is incorrect");

            if (!UserParam.IsGenerateNewToken)
            {
                token = await GetTokenByUserId();
            }

            if (token == null)
            {
                await CreateNewToken(UserParam);
            }

            return token.TokenKey;
        }

        public async Task<bool> IsTokenValid(string TokenKey)
        {
            token = await this.GetToken(TokenKey);

            return (token != null && token.ExpirationDate > DateTime.UtcNow) ? true : false;
        } 
        #endregion

        #region Private Methods       

        private async Task<Token> GetToken(string TokenKey)
        {
            return await context.Token.OrderByDescending(x => x.ExpirationDate)
                                      .FirstOrDefaultAsync(x => x.TokenKey == token.TokenKey &&
                                                                x.DeletedDate == null);
        }

        private async Task<Token> GetTokenByUserId()
        {
            return await context.Token.OrderByDescending(x => x.ExpirationDate)
                                      .FirstOrDefaultAsync(x => x.UserId == user.Id &&
                                                                x.ExpirationDate >= DateTime.UtcNow);
        }

        private async Task CreateNewToken(UserTokenRequest UserParam)
        {
            SetTokenData(UserParam.BrowserCapabilities, UserParam.IpAdderess, UserParam.HostUrl);

            token.TokenKey = GenerateNewToken();

            await context.Token.AddAsync(token);
            await context.SaveChangesAsync();
        }

        private void SetTokenData(string BrowserCapabilities, string IpAddress, string hostUrl)
        {
            token = new JWT_Auth.Microservice.Entities.Token();
            token.BrowserCapabilities = BrowserCapabilities;
            token.Ip = IpAddress;
            token.CreatedDate = DateTime.UtcNow;
            token.ExpirationDate = DateTime.UtcNow.AddHours(double.Parse(configuration.GetSection("AppSettings:ExpireAfterHours").Value));
            token.SecurityTypeId = (byte)SecurityTypes.HmacSha256;
            token.SystemName = hostUrl;
            token.UserId = user.Id;
            token.Issuer = configuration.GetSection("AppSettings:Issuer").Value;
            token.Audience = configuration.GetSection("AppSettings:Audience").Value;            
        }

        private string GenerateNewToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Secret").Value);
            
            lstUserRoles = user.UserUserRole.Select(x => x.UserRole.Name);
            var userRoles = JsonConvert.SerializeObject(lstUserRoles, 
                                    new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = token.Audience,

                Issuer = token.Issuer,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}", ClaimValueTypes.String),                                        
                    new Claim(ClaimTypes.Role, userRoles, ClaimValueTypes.String),
                }),
                Expires = token.ExpirationDate,
                IssuedAt = token.CreatedDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            foreach (var userRole in lstUserRoles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole, ClaimValueTypes.String));
            }            

            var securedToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securedToken);
        }
        #endregion
    }
}
