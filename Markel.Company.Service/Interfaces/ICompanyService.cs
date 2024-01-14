namespace Markel.Company.Service.Interfaces;

public interface ICompanyService
{
    Task<Domain.Dtos.Company?> Get(int id);
    Task<Domain.Dtos.Company?> Create(Domain.Dtos.Company company);
    Task<bool> Update(int id, Domain.Dtos.Company company);
}
