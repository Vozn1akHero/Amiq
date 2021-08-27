using Amiq.Business.Utils;
using Amiq.DataAccess.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Chat.BsRules
{
    public class ChatDataShouldBeVisibleForInterlocutor : IBsRule
    {
        private DaChat _daChat = new DaChat();

        public string ErrorContent => "Chat data is unavailable for request issuer";

        public bool CheckBsRule()
        {
            throw new NotImplementedException();
        }
    }
}
