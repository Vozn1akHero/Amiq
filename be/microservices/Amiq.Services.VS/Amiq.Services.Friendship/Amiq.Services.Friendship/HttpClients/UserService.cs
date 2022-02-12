using Amiq.Services.Common.Contracts;
using Amiq.Services.Friendship.Cache.Redis;
using Amiq.Services.Friendship.Contracts.User;
using Microsoft.AspNetCore.WebUtilities;

namespace Amiq.Services.Friendship.HttpClients
{
    public class UserService
    {
        private HttpClient _httpClient;
        private UserCacheService _userCacheService;
        private IConfiguration _configuration;

        public UserService(HttpClient httpClient, UserCacheService userCacheService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _userCacheService = userCacheService;
            _configuration = configuration;
        }

        public async Task<DtoBasicUserInfo?> GetUserByIdAsync(int userId)
        {
            DtoBasicUserInfo cachedUserData = _userCacheService.GetUserById(userId);
            if (cachedUserData != null)
            {
                return cachedUserData;
            }

            var result = await _httpClient.GetAsync($"{_configuration.GetValue<string>("Services:UserService")}/api/user/basic-user-data/{userId}");

            if (result.IsSuccessStatusCode)
            {

                return await result.Content.ReadFromJsonAsync<DtoBasicUserInfo>();
            }

            return null;
        }

        public async Task<IEnumerable<DtoUserSearchResult>?> SearchAsync(int issuerId, string text, DtoPaginatedRequest paginatedRequest)
        {
            _httpClient.DefaultRequestHeaders.Add("Amiq-UserId", issuerId.ToString());

            var uri = QueryHelpers.AddQueryString($"{_configuration.GetValue<string>("Services:UserService")}/api/user/search", new Dictionary<string, string?>()
            {
                ["text"] = text,
                ["page"] = paginatedRequest.Page.ToString(),
                ["count"] = paginatedRequest.Count.ToString()
            });

            var res = await _httpClient.GetAsync(uri);

            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<List<DtoUserSearchResult>>();
            }

            return null;
        }
    }
}
