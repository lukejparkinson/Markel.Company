namespace Markel.Company.Domain.Entities;

public sealed class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? AddressOne { get; set; }
    public string? AddressTwo { get; set; }
    public string? AddressThree { get; set; }
    public string? Postcode { get; set; }
    public string? Country { get; set; }
    public bool Active { get; set; }
    public DateTime InsuranceEndDate { get; set; }
}
