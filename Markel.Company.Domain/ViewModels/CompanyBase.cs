using System.ComponentModel.DataAnnotations;

namespace Markel.Company.Domain.ViewModels;

public class CompanyBase
{
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    public string? Name { get; set; }
    [Required]
    [MinLength(1)]
    public string? AddressOne { get; set; }
    [Required]
    [MinLength(1)]
    public string? AddressTwo { get; set; }
    [Required]
    [MinLength(1)]
    public string? AddressThree { get; set; }
    [Required]
    [RegularExpression(@"^(([A-Z][0-9]{1,2})|(([A-Z][A-HJ-Y][0-9]{1,2})|(([A-Z][0-9][A-Z])|([A-Z][A-HJ-Y][0-9]?[A-Z])))) [0-9][A-Z]{2}$")]
    public string? Postcode { get; set; }
    public string? Country { get; set; }
    public bool Active { get; set; }
    public DateTime InsuranceEndDate { get; set; }
}
