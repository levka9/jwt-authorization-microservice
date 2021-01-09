using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWT.Auth.Entities;
using JWT.Auth.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using JWT.Auth.Models.Enums;
using JWT.Auth.Modules.Interafaces;
using JWT.Auth.Entities.Context;

namespace JwtWebTokenSerice.Modules
{
    public class JwtTokenModule : IJwtTokenValidator
    {
        #region Properties
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        readonly AppSettings appSettings;

        long userId;
        IEnumerable<string> userRoles;
        JWTAuthContext context;
        JWT.Auth.Entities.Token token;

        public JWT.Auth.Entities.Token Token 
        {
            get { return token; }
        }
        #endregion

        #region Constructors
        public JwtTokenModule(string TokenKey, JWTAuthContext Context)
        {
            token = new JWT.Auth.Entities.Token();
            token.TokenKey = TokenKey;
            context = Context;
        }

        public JwtTokenModule(long UserId, IEnumerable<string> UserRoles,
                              JWTAuthContext Context,
                              IOptions<AppSettings> AppSettings)
        {            
            userId = UserId;
            userRoles = UserRoles;
            context = Context;

            appSettings = AppSettings.Value;
        }
        #endregion

        #region Methods
        public async Task<string> GetToken(string BrowserCapabilities, string IpAdderess, string HostUrl, bool IsGenerateNewToken = false)
        {
            if (!IsGenerateNewToken)
            {
                token = await GetTokenByUserId(IsGenerateNewToken);
            }

            if (token == null)
            {
                SetTokenData(BrowserCapabilities, IpAdderess, HostUrl);

                token.TokenKey = GenerateNewToken();

                await context.Token.AddAsync(token);
                await context.SaveChangesAsync();
            }

            return token.TokenKey;
        }

        public async Task<bool> IsTokenValid()
        {
            token = await this.GetToken();

            return (token != null && token.ExpirationDate > DateTime.UtcNow) ? true : false;
        }

        private async Task<Token> GetToken()
        {
            return await context.Token.OrderByDescending(x => x.ExpirationDate)
                                          .FirstOrDefaultAsync(x => x.TokenKey == token.TokenKey &&
                                                                    x.DeletedDate == null);
        }

        private async Task<Token> GetTokenByUserId(bool IsForceNew)
        {
            return await context.Token.OrderByDescending(x => x.ExpirationDate)
                                      .FirstOrDefaultAsync(x => x.UserId == userId &&
                                                                x.ExpirationDate >= DateTime.UtcNow);
        }

        private void SetTokenData(string BrowserCapabilities, string IpAddress, string hostUrl)
        {
            token = new JWT.Auth.Entities.Token();
            token.BrowserCapabilities = BrowserCapabilities;
            token.Ip = IpAddress;
            token.CreatedDate = DateTime.UtcNow;
            token.ExpirationDate = DateTime.UtcNow.AddHours(appSettings.ExpireAfterHours);
            token.SecurityTypeId = (byte)SecurityTypes.HmacSha256;
            token.SystemName = hostUrl;
            token.UserId = userId;
            token.Issuer = appSettings.Issuer;
            token.Audience = appSettings.Audience;
        }

        private string GenerateNewToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = token.Audience,

                Issuer = token.Issuer,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, token.UserId.ToString(), ClaimValueTypes.Integer),                 
                }),
                Expires = token.ExpirationDate,
                IssuedAt = token.CreatedDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            foreach (var UserUserRole in token.User.UserUserRole)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, UserUserRole.UserRole.ToString(), ClaimValueTypes.String));
            }            

            var securedToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securedToken);
        }
        #endregion
    }
}
