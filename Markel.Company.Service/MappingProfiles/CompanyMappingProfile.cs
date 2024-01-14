using AutoMapper;

namespace Markel.Company.Service.MappingProfiles;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        CreateMap<Domain.Entities.Company, Domain.Dtos.Company>();
        CreateMap<Domain.Dtos.Company, Domain.Entities.Company>();
        CreateMap<Domain.ViewModels.Company, Domain.Dtos.Company>();
        CreateMap<Domain.ViewModels.CreateCompany, Domain.Dtos.Company>();
        CreateMap<Domain.ViewModels.UpdateCompany, Domain.Dtos.Company>();
        CreateMap<Domain.Dtos.Company, Domain.ViewModels.Company>();
    }
}
