using Amiq.ApiGateways.WebApp.Contracts.Auth;
using Amiq.ApiGateways.WebApp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.ApiGateways.WebApp.Core.Auth
{
    internal struct JwtRegisteredClaimNamesEx
    {
        public const string UserName = "userName";
        public const string UserSurname = "userSurname";
        public const string UserId = "userId";
    }

    public class JwtExtensions2
    {
        public static TokenValidationParameters JwtValidationParameters
        { 
            get => new TokenValidationParameters {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = StaticContextConfigurationProvider.GetAppSetting("Jwt:Issuer"),
                ValidAudience = StaticContextConfigurationProvider.GetAppSetting("Jwt:Issuer"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticContextConfigurationProvider.GetAppSetting("Jwt:PrivateKey")))
            };
        }

        public static AccessToken2 GenerateJSONWebToken(DtoJwtBase jwtBase)
        {
            AccessToken2 accessToken = new();
            accessToken.ExpiresAt = DateTime.UtcNow.AddDays(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(StaticContextConfigurationProvider.GetAppSetting("Jwt:PrivateKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwtBase.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, jwtBase.UserEmail),
                    new Claim(JwtRegisteredClaimNamesEx.UserName, jwtBase.UserName.ToString()),
                    new Claim(JwtRegisteredClaimNamesEx.UserSurname, jwtBase.UserSurname.ToString()),
                    // Ocelot sub claim translation problem fix
                    //new Claim(JwtRegisteredClaimNamesEx.UserId, jwtBase.UserId.ToString())
                }),
                Expires = accessToken.ExpiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            accessToken.Token = tokenHandler.WriteToken(token);

            return accessToken;
        }

        public static bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(StaticContextConfigurationProvider.GetAppSetting("Jwt:Key"));
            try
            {
                tokenHandler.ValidateToken(token, JwtValidationParameters, out SecurityToken validatedToken);
                return validatedToken != null;
            } catch (SecurityTokenExpiredException)
            {
                return false;
            }
            //return false;
        }

        public static DtoJwtStoredUserInfo GetJwtStoredUserInfo(string token)
        {
            DtoJwtStoredUserInfo result = new();
            var tokenHandler = new JwtSecurityTokenHandler();
            
            tokenHandler.ValidateToken(token, JwtValidationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            int userId = int.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
            string name = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNamesEx.UserName).Value;
            string surname = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNamesEx.UserSurname).Value;
            string email = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value;
            
            result.UserId = userId;
            result.Email = email;
            result.UserName = name;
            result.UserSurname = surname;   

            return result;
        }
    }
}
