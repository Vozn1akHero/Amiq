﻿using System;

namespace Amiq.Services.User.Core.Auth
{
    public class AccessToken2
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
