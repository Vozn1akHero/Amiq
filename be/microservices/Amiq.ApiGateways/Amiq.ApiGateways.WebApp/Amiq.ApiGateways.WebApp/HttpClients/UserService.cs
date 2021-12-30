using Amiq.ApiGateways.WebApp.Cache.Redis;
using Amiq.ApiGateways.WebApp.Contracts.User;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Amiq.ApiGateways.WebApp.HttpClients
{
    public class UserService
    {
        //private const string USER_SERVICE = "http://user-clusterip-srv:80/";
        private const string USER_SERVICE = "http://localhost:5011";
        private const string FRIENDSHIP_SERVICE = "http://friendship-clusterip-srv/api/";

        private HttpClient _httpClient;
        private UserCacheService _userCacheService;

        public UserService(HttpClient httpClient, UserCacheService userCacheService)
        {
            _httpClient = httpClient;
            _userCacheService = userCacheService;
        }

        public DtoBasicUserInfo GetUserById(string requestCreatorId, int userId)
        {
            DtoBasicUserInfo cachedUserData = _userCacheService.GetUserById(userId);
            if(cachedUserData != null)
            {
                return cachedUserData;
            }

            DtoBasicUserInfo user = null;
            //_httpClient.DefaultRequestHeaders.Add("Amiq-UserId", requestCreatorId);
            var responseTask = _httpClient.GetAsync($"{USER_SERVICE}/api/user/basic-user-data/{userId}");
            
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {

                var readTask = result.Content.ReadFromJsonAsync<DtoBasicUserInfo>();
                readTask.Wait();

                user = readTask.Result;

                _userCacheService.StoreUserData(user);
            }

            return user;
        }


    }
}
