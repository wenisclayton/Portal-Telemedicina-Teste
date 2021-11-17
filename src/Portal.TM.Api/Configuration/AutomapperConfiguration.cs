using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Portal.TM.Api.ViewModels;
using Portal.TM.Business.Entities;

namespace Portal.TM.Api.Configuration;
public static class AutomapperConfiguration
{
    public static void ConfigureAutomapper(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = new Mapper(mappingConfig);
        services.TryAddSingleton(mapper);
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductRegister>().ReverseMap();
            
        }


    }
}
