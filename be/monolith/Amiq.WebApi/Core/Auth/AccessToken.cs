using System;

namespace Amiq.WebApi.Core.Auth
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
