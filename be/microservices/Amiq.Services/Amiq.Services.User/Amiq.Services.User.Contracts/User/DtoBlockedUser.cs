using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.User.Contracts.User
{
    public class DtoBlockedUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
