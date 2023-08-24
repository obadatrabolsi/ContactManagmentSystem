using AutoMapper;

namespace CMS.Core.Mappers
{
    /// <summary>
    /// Auto mapper factory to create or get an instance of mapper
    /// </summary>
    public static class AutoMapperFactory
    {
        private static IMapper instance;
        private static MapperConfiguration mapperConfiguration;

        /// <summary>
        /// Return or create a mapper instance
        /// </summary>
        public static IMapper Mapper => instance ?? (instance = mapperConfiguration.CreateMapper());

        /// <summary>
        /// Setup auto mapper configuration
        /// </summary>
        /// <param name="config"></param>
        public static void AddAutoMapper(MapperConfiguration config)
        {
            mapperConfiguration =config;
        }
    }
}
