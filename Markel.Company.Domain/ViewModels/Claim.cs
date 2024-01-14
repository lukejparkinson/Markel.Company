namespace Markel.Company.Domain.ViewModels;

public class Claim : ClaimBase
{
    public double ClaimAgeDays => (DateTime.UtcNow - ClaimDate).TotalDays;
}
