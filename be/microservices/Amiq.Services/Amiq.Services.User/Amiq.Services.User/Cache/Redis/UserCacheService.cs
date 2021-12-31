using Amiq.Services.User.Contracts.User;
using System.Text.Json;

namespace Amiq.Services.User.Cache.Redis
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

        public void StoreUserData(DtoBasicUserInfo dtoBasicUserInfo)
        {
            RedisConnectionFactory.Db.StringSet(dtoBasicUserInfo.UserId.ToString(), JsonSerializer.Serialize(dtoBasicUserInfo));
        }
    }
}
