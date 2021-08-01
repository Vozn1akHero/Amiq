using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Mapping
{
    public class APProfile : Profile
    {
        public void CreateTwoWayMap<T1, T2>() {
            CreateMap(typeof(T1), typeof(T2));
            CreateMap(typeof(T2), typeof(T1));
        }
    }
}
