namespace Amiq.Services.Base.Auth
{
    public class DtoJwtStoredUserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
    }
}
