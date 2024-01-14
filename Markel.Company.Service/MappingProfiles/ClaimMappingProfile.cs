using AutoMapper;

namespace Markel.Company.Service.MappingProfiles;

public class ClaimMappingProfile : Profile
{
    public ClaimMappingProfile()
    {
        CreateMap<Domain.Entities.Claim, Domain.Dtos.Claim>();
        CreateMap<Domain.Dtos.Claim, Domain.Entities.Claim>();
        CreateMap<Domain.ViewModels.Claim, Domain.Dtos.Claim>();
        CreateMap<Domain.ViewModels.CreateClaim, Domain.Dtos.Claim>();
        CreateMap<Domain.ViewModels.UpdateClaim, Domain.Dtos.Claim>();
        CreateMap<Domain.Dtos.Claim, Domain.ViewModels.Claim>();
    }
}
