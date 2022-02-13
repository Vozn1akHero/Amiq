using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.User.Contracts.Auth
{
    public class DtoJwtStoredUserInfo2
    {
        public int UserId { get; set; }
        public string UserName {  get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
    }
}
