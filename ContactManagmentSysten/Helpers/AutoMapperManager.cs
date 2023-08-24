using AutoMapper;
using CMS.Core.Mappers;
using System.Reflection;

namespace CMS.Api.Helpers
{
    public class AutoMapperManager
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.RegisterAllProfiles(Assembly.Load("CMS.Domain"));
            });
        }
    }
}
