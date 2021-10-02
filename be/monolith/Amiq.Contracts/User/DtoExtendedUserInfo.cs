using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Contracts.User
{
    public class DtoExtendedUserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public DateTime Birthdate { get; set; }
        public string ShortDescription { get; set; }
        public IEnumerable<DtoUserDescriptionBlock> UserDescriptionBlocks { get; set; }
    }
}
