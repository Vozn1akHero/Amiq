using System;

namespace Amiq.Contracts.User
{
    public class DtoUserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
