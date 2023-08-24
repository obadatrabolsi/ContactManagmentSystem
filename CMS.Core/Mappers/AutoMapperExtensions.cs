using AutoMapper;
using CMS.Core.Models;
using System.Reflection;

namespace CMS.Core.Mappers
{
    public static class AutoMapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object source) where TDestination : class
        {
            return AutoMapperFactory.Mapper.Map(source, source.GetType(), typeof(TDestination)) as TDestination;
        }
        public static TDestination MapTo<TDestination>(this object source, TDestination destination) where TDestination : class
        {
            return AutoMapperFactory.Mapper.Map(source, destination, source.GetType(), typeof(TDestination)) as TDestination;
        }
        public static PagedResponse<TDestination> MapToPaginatedList<TSource, TDestination>(this PagedResponse<TSource> model)
        {
            var data = model.List.MapTo<List<TDestination>>();

            return new()
            {
                TotalCount = model.TotalCount,
                List = data,

                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                TotalPages = model.TotalPages,
                CurrentPage = model.CurrentPage,
                HasMorePages = model.HasMorePages,
                NextPage = model.NextPage,
                PaginationEnabled = model.PaginationEnabled,
                PrevPage = model.PrevPage,
            };
        }
        public static void RegisterAllProfiles(this IMapperConfigurationExpression cfg, Assembly assembly)
        {
            var baseType = typeof(Profile);

            var domainEntities = assembly.GetTypes()
                                         .Where(t => t.IsClass && !t.IsAbstract &&  t.IsSubclassOf(baseType))
                                         .Select(t => Activator.CreateInstance(t) as Profile)
                                         .ToList();

            domainEntities.ForEach(cfg.AddProfile);
        }
    }
}
