namespace Markel.Company.Domain.ViewModels;

public class Company : CompanyBase
{
    public bool HasActiveInsurancePolicy => InsuranceEndDate > DateTime.UtcNow;
}
