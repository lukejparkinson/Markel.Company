using Markel.Company.Repository.Interfaces;

namespace Markel.Company.Repository;

public sealed class CompanyRepository : ICompanyRepository
{
    private readonly CompanyContext _companyContext;
    public CompanyRepository(CompanyContext companyContext)
    {
        ArgumentNullException.ThrowIfNull(companyContext, nameof(companyContext));

        _companyContext = companyContext;
    }

    public async Task<Domain.Entities.Company?> Get(int id) =>
        await _companyContext.Companies.FindAsync(id);

    public async Task<Domain.Entities.Company?> Create(Domain.Entities.Company company)
    {
        var record = await _companyContext.Companies.AddAsync(company);

        var success = _companyContext.SaveChanges() > 0;

        return success ? record.Entity : null;
    }

    public async Task<bool> Update(int id, Domain.Entities.Company company)
    {
        var record = await Get(id);

        if (record != null)
        {
            record.Name = company.Name;
            record.AddressOne = company.AddressOne;
            record.AddressTwo = company.AddressTwo;
            record.AddressThree = company.AddressThree;
            record.Postcode = company.Postcode;
            record.Country = company.Country;
            record.Active = company.Active;
            record.InsuranceEndDate = company.InsuranceEndDate;

            return _companyContext.SaveChanges() > 0;
        }

        return false;
    }
}
