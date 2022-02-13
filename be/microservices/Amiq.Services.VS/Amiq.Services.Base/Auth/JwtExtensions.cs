using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Amiq.Services.Base.Auth
{
    internal struct JwtRegisteredClaimNamesEx
    {
        public const string UserName = "userName";
        public const string UserSurname = "userSurname";
        public const string UserId = "userId";
    }

    public class JwtExtensions
    {
        private const string Audience = "Amiq.com";
        private const string SigningKey = "kdas8dad8ah2d10123daslkd2312l213j1k31dmasdjklk123";

        public static TokenValidationParameters JwtValidationParameters
        {
            get => new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Audience,
                ValidAudience = Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey))
            };
        }

        public static AccessToken GenerateJSONWebToken(DtoJwtBase jwtBase)
        {
            AccessToken accessToken = new();
            accessToken.ExpiresAt = DateTime.UtcNow.AddDays(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(SigningKey);
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
            }
            catch (SecurityTokenExpiredException)
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
