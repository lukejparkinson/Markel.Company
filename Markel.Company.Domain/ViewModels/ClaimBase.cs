using Markel.Company.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Markel.Company.Domain.ViewModels;

public class ClaimBase
{
    public Guid Ucr { get; set; }
    [Required]
    public int CompanyId { get; set; }
    [Required]
    public DateTime ClaimDate { get; set; }
    [Required]
    public DateTime LossDate { get; set; }
    [Required]
    public string? AssuredName { get; set; }
    [Required]
    public decimal IncurredLoss { get; set; }
    [Required]
    public bool Closed { get; set; }
    [Required]
    public ClaimType ClaimType { get; set; }
}
