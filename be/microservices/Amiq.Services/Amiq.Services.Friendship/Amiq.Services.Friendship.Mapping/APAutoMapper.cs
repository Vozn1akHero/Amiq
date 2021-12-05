using AutoMapper;
using System.Reflection;

namespace Amiq.Services.Friendship.Mapping
{
    public static class AmiqFriendshipAutoMapper
    {
        private static IMapper _mapper;
        private static bool _isInitialized;

        public static IMapper Instance
        {
            get => _mapper;
        }

        public static void Initialize()
        {
            if (!_isInitialized)
            {
                ConfigureMapper();
                _isInitialized = true;
            }
        }

        private static void ConfigureMapper()
        {
            var profiles = Assembly.GetCallingAssembly().GetTypes()
                                            .Where(t => t.IsSubclassOf(typeof(Profile)))
                                            .Select(t => (Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
                cfg.AllowNullCollections = false;
            });

            _mapper = config.CreateMapper();
        }
    }
}
