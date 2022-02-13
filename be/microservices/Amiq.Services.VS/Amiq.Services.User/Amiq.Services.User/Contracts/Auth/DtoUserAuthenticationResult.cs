using Amiq.Services.Base.Auth;

namespace Amiq.Services.User.Contracts.Auth
{
    public class DtoUserAuthenticationResult
    {
        public bool Success { get; set; }
        public DtoJwtBase JwtBase { get; set; }
    }
}
