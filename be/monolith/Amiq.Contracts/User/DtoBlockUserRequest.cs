using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.User
{
    public class DtoUserBlockRequest
    {
        public int IssuerId { get; set; }
        public int DestUserId { get; set; }
    }
}
