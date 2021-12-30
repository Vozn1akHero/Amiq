using Amiq.Services.User.Contracts.Friendship;

namespace Amiq.Services.User.HttpClients
{
    public class FriendshipService
    {
        //private const string FRIENDSHIP_SERVICE = "http://friendship-clusterip-srv/api/";
        private const string FRIENDSHIP_SERVICE = "http://localhost:49643";
        private HttpClient _httpClient;

        public FriendshipService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public DtoFriendshipStatus GetFriendshipStatusBetweenUsers(string requestCreatorId, int sUserId)
        {
            DtoFriendshipStatus friendshipStatus = null;

            _httpClient.DefaultRequestHeaders.Add("Amiq-UserId", requestCreatorId);
            var responseTask = _httpClient.GetAsync($"{FRIENDSHIP_SERVICE}/api/friendship/friendship-status/{sUserId}");
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
