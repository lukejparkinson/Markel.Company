using AutoMapper;
using Markel.Company.Service.MappingProfiles;

namespace Markel.Company.Service;

public class MapperConfig : MapperConfiguration
{
    public MapperConfig() : base(Configuration) { }
    public static Action<IMapperConfigurationExpression> Configuration =>
        (cfg) =>
        {
            cfg.AddProfile<CompanyMappingProfile>();
            cfg.AddProfile<ClaimMappingProfile>();
        };
}
