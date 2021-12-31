using Amiq.Services.User.Contracts.Friendship;

namespace Amiq.Services.User.HttpClients
{
    public class FriendshipService
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;

        public FriendshipService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public DtoFriendshipStatus GetFriendshipStatusBetweenUsers(string requestCreatorId, int sUserId)
        {
            DtoFriendshipStatus friendshipStatus = null;

            _httpClient.DefaultRequestHeaders.Add("Amiq-UserId", requestCreatorId);
            var responseTask = _httpClient.GetAsync($"{_configuration.GetValue<string>("Services:FriendshipService")}/api/friendship/friendship-status/{sUserId}");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadFromJsonAsync<DtoFriendshipStatus>();
                readTask.Wait();

                friendshipStatus = readTask.Result;
            }

            return friendshipStatus;
        }
    }
}
