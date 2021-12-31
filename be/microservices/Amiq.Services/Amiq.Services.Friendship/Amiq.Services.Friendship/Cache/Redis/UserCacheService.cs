using Amiq.Services.Friendship.Contracts.User;
using System.Text.Json;

namespace Amiq.Services.Friendship.Cache.Redis
{
    public class UserCacheService
    {
        public DtoBasicUserInfo GetUserById(int userId)
        {
            var cachedUser = RedisConnectionFactory.Db.StringGet(userId.ToString());
            if (cachedUser.HasValue)
            {
                var cachedUserObj = JsonSerializer.Deserialize<DtoBasicUserInfo>(cachedUser);
                return cachedUserObj;
            }
            return null;
        }
    }
}
