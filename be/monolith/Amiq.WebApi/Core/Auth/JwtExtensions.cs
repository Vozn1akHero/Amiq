using Amiq.Contracts.Auth;
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

namespace Amiq.Core.Auth
{
    public struct JwtRegisteredClaimNamesEx
    {
        public const string UserId = "userId";
    }

    public class JwtExtensions
    {
        private static IConfiguration _config;

        public JwtExtensions(IConfiguration config)
        {
            _config = config;
        }

        public static TokenValidationParameters JwtValidationParameters
        { get => new TokenValidationParameters {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };
        }

        public static string GenerateJSONWebToken(int userId, string userEmail)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Things to be included and encoded in the token
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userEmail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userEmail),
                    new Claim(JwtRegisteredClaimNamesEx.UserId, userId.ToString())
                }),
                // Token will expire 2 hours from which it was created
                Expires = DateTime.UtcNow.AddHours(2),
                //
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static DtoJwtStoredUserInfo GetJwtStoredUserInfo(string token)
        {
            DtoJwtStoredUserInfo result = new();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            tokenHandler.ValidateToken(token, JwtValidationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
            string name = jwtToken.Claims.First(x => x.Type == "Name").Value;
            string surname = jwtToken.Claims.First(x => x.Type == "Surname").Value;
            string email = jwtToken.Claims.First(x => x.Type == "Email").Value;
            
            return result;
        }
    }
}
