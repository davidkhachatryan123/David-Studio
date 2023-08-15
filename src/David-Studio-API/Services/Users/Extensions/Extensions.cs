using AutoMapper;
using Users.Mappings;

namespace Users.Extensions
{
    public static class Extensions
    {
        public static void ConfigureMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<UsersMappingProfile>();
            });

            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}

