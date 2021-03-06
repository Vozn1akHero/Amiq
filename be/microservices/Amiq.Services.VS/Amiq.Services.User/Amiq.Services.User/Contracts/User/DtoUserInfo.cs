using System;

namespace Amiq.Services.User.Contracts.User
{
    public class DtoUserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string AvatarPath { get; set; }
        public DateTime Birthdate { get; set; }
        public string ShortDescription { get; set; }
    }
}
