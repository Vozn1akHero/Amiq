﻿using Amiq.Services.Friendship.BusinessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Friendship.BusinessLayer.BsRules
{
    public class BsRuleRequestIssuerCannotBeBlockedByReceiver : IBsRule
    {
        public string ErrorContent => "Użytkownik jest zablokowany przez innego użytkownika";

        public bool CheckBsRule()
        {
            throw new NotImplementedException();
        }
    }
}
