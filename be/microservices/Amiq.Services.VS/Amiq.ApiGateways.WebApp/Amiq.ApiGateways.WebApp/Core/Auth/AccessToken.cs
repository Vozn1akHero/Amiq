using System;

namespace Amiq.ApiGateways.WebApp.Core.Auth
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
