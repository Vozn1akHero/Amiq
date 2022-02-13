namespace Amiq.ApiGateways.WebApp.Contracts
{
    public class DtoBlockedUserQuery
    {
        public int BlockedUserId { get; set; }
        public int BlockedBy { get; set; }
    }
}
