using Markel.Company.Domain.Dtos;

namespace Markel.Company.Service.Interfaces;

public interface IClaimService
{
    Task<Claim?> Get(Guid ucr);
    Task<IEnumerable<Claim>> GetAllForCompany(int companyId);
    Task<Claim?> Create(Claim claim);
    Task<bool> Update(Guid ucr, Claim claim);
}
