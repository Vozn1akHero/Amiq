using System;

namespace Amiq.Services.User.Core.Auth
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
