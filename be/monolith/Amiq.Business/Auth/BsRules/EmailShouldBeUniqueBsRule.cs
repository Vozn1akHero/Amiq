﻿using Amiq.Business.Utils;
using Amiq.DataAccess.Auth;

namespace Amiq.Business.Auth.Rules
{
    public class EmailShouldBeUniqueBsRule : IBsRule
    {
        private IDaAuth _daAuth;
        private string _email;

        public EmailShouldBeUniqueBsRule(IDaAuth daAuth, string email)
        {
            _daAuth = daAuth;
            _email = email;
        }

        public string ErrorContent => "Email already taken";

        public bool CheckBsRule()
        {
            return _daAuth.EmailExists(_email);
        }
    }
}