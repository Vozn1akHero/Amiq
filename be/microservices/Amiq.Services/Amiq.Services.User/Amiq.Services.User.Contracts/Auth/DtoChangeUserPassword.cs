namespace Amiq.Services.User.Contracts.Auth
{
    public class DtoChangeUserPassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
