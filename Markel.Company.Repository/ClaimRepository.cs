using Markel.Company.Domain.Entities;
using Markel.Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Markel.Company.Repository;

public class ClaimRepository : IClaimRepository
{
    private readonly CompanyContext _companyContext;
    public ClaimRepository(CompanyContext companyContext)
    {
        ArgumentNullException.ThrowIfNull(companyContext, nameof(companyContext));

        _companyContext = companyContext;
    }

    public async Task<Claim?> Get(Guid ucr)
        => await _companyContext.Claims.FindAsync(ucr);

    public async Task<IEnumerable<Claim>> GetAllForCompany(int companyId)
        => await _companyContext.Claims
        .Where(x => x.CompanyId == companyId)
        .ToListAsync();

    public async Task<Claim?> Create(Claim claim)
    {
        var record = await _companyContext.Claims.AddAsync(claim);

        var success = _companyContext.SaveChanges() > 0;

        return success ? record.Entity : null;
    }

    public async Task<bool> Update(Guid ucr, Claim claim)
    {
        var record = await Get(ucr);

        if (record != null)
        {
            record.ClaimDate = claim.ClaimDate;
            record.LossDate = claim.LossDate;
            record.AssuredName = claim.AssuredName;
            record.IncurredLoss = claim.IncurredLoss;
            record.Closed = claim.Closed;
            record.ClaimType = claim.ClaimType;

            return _companyContext.SaveChanges() > 0;
        }

        return false;
    }
}
