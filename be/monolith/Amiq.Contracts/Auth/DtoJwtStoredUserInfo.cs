using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.Auth
{
    public class DtoJwtStoredUserInfo
    {
        public int UserId { get; set; }
        public string UserName {  get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
    }
}
