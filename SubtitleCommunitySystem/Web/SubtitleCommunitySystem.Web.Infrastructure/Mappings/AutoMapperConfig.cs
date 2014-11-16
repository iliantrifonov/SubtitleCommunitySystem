namespace SubtitleCommunitySystem.Web.Infrastructure.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    public class AutoMapperConfig
    {
        public void Execute(Assembly assembly)
        {
            //var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            var types = assembly.GetExportedTypes();

            LoadStandardMappings(types);

            LoadCustomMappings(types);

            LoadApplicationCustomMappings();
        }

        private void LoadApplicationCustomMappings()
        {
            //Mapper.CreateMap<MovieInputModel, Movie>();
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            var mapsTo = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = t,
                            Destination = i.GetGenericArguments()[0]
                        }).ToArray();

            foreach (var map in maps)
            {
                Mapper.CreateMap(map.Source, map.Destination);
            }

            foreach (var map in mapsTo)
            {
                Mapper.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }
    }
}