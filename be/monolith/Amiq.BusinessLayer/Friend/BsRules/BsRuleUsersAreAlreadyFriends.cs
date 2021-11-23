using Amiq.Business.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Friend.BsRules
{
    public class BsRuleUsersAreAlreadyFriends : IBsRuleAsync
    {
        public string ErrorContent => throw new NotImplementedException();

        public Task<bool> CheckBsRuleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
