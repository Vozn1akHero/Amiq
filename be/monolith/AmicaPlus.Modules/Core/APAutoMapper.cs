using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Modules.Mapping
{
    public sealed class APAutoMapper
    {
        private static IMapper _mapper;

        public static IMapper Instance => _mapper;

        static APAutoMapper()
        {
            ConfigureMapper();
        }

        private static void ConfigureMapper()
        {
            IEnumerable<Profile> exporters = typeof(Profile).Assembly.GetTypes()
                                            .Where(t => t.IsSubclassOf(typeof(Profile)))
                                            .Select(t => (Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg => {
                foreach (var exporter in exporters)
                {
                    cfg.AddProfile(exporter);
                }                
            });

            _mapper = config.CreateMapper();
        }

    }
}
