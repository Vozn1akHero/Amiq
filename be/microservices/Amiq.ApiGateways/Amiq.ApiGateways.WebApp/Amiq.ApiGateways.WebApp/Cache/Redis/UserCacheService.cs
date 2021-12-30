using Amiq.ApiGateways.WebApp.Contracts.User;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Amiq.ApiGateways.WebApp.Cache.Redis
{
    public class UserCacheService
    {
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public DtoBasicUserInfo GetUserById(int userId)
        {
            var cachedUser = RedisConnectionFactory.Db.StringGet(userId.ToString());
            if(cachedUser.HasValue)
            {
                var cachedUserObj = JsonSerializer.Deserialize<DtoBasicUserInfo>(cachedUser);
                return cachedUserObj;
            }
            return null;
        }

        public void StoreUserData(DtoBasicUserInfo dtoBasicUserInfo)
        {
            //await _semaphore.WaitAsync();

            RedisConnectionFactory.Db.StringSet(dtoBasicUserInfo.UserId.ToString(), JsonSerializer.Serialize(dtoBasicUserInfo));

            //_semaphore.Release();
        }
    }
}
