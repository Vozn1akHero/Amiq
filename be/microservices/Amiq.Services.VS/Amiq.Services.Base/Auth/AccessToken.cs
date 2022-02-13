using System;

namespace Amiq.Services.Base.Auth
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
