namespace Amiq.ApiGateways.WebApp.Contracts.Auth
{
    public class DtoJwtBase
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
    }
}
