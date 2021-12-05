using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.User.Contracts.Auth
{
    public class DtoUserAuthenticationResult
    {
        public bool Success { get; set; }
        public DtoJwtBase JwtBase { get; set; }
    }
}
