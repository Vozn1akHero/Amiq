﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Chat
{
    public class DtoChatMessage
    {
        public int ChatId { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public int ReceiverId { get; set; }
    }
}
