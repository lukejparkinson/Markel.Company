using Markel.Company.Domain.Enums;

namespace Markel.Company.Domain.Dtos;

public class Claim
{
    public Guid Ucr { get; set; }
    public int CompanyId { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime LossDate { get; set; }
    public string? AssuredName { get; set; }
    public decimal IncurredLoss { get; set; }
    public bool Closed { get; set; }
    public ClaimType ClaimType { get; set; }
}
