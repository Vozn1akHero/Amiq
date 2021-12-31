using Amiq.Services.Friendship.Cache.Redis;
using Amiq.Services.Friendship.Contracts.User;

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
    }
}
