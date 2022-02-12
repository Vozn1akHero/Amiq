using Amiq.Services.User.Contracts.Friendship;
using System.Text;
using System.Text.Json;

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
            var responseTask = _httpClient.GetAsync($"{_configuration.GetValue<string>("Services:FriendshipService")}/api/friendship/pb-friendship-status/{sUserId}");
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

        /*public async Task<IEnumerable<DtoFriendshipStatus>?> GetFriendshipsForUserAsync(string requestCreatorId, IEnumerable<int> userIds)
        {
            _httpClient.DefaultRequestHeaders.Add("Amiq-UserId", requestCreatorId);

            var res = await _httpClient.PostAsync($"{_configuration.GetValue<string>("Services:FriendshipService")}/api/friendship/pb-friendship-status",
                new StringContent(JsonSerializer.Serialize(new { UserIds = userIds}), Encoding.UTF8, "application/json"));

            if (res.IsSuccessStatusCode)
            {
               return await res.Content.ReadFromJsonAsync<List<DtoFriendshipStatus>>();
            }

            return null;
        }*/
    }
}
