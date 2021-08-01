using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Mapping
{
    public static class APAutoMapper
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
            });

            _mapper = config.CreateMapper();
        }
    }
}
