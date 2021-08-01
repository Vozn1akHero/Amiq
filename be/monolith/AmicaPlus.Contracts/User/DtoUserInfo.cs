using System;

namespace AmicaPlus.Contracts.User
{
    public class DtoUserInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
