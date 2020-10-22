using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MapHelper
    {
        public static Mapper Mapping<T, Y>()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, Y>());
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
