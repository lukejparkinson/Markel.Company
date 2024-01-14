using Markel.Company.Domain.Entities;

namespace Markel.Company.Repository.Interfaces;

public interface IClaimRepository
{
    Task<Claim?> Get(Guid ucr);
    Task<IEnumerable<Claim>> GetAllForCompany(int companyId);
    Task<Claim?> Create(Claim claim);
    Task<bool> Update(Guid ucr, Claim claim);

}
