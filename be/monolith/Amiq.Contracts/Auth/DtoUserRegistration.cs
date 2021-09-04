using Amiq.Enums;
using System;

namespace Amiq.Contracts.Auth
{
    public class DtoUserRegistration
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthdate { get; set; }
        public string Sex { get; set; }
    }
}
