namespace Markel.Company.Repository.Interfaces;

public interface ICompanyRepository
{
    Task<Domain.Entities.Company?> Get(int id);
    Task<Domain.Entities.Company?> Create(Domain.Entities.Company company);
    Task<bool> Update(int id, Domain.Entities.Company company);
}
