using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Markel.Company.Service;

namespace Markel.Company.Test.Service.MappingProfiles;

public class CompanyMappingProfileTests
{
    private static IMapper _mapper => new Mapper(new MapperConfig());

    #region Dto to ViewModel

    [Theory, AutoData]
    public void Map_Dto_To_ViewModel_ReturnsValid(Domain.Dtos.Company dto)
    {
        //Arrange
        dto.InsuranceEndDate = DateTime.UtcNow.AddDays(-1);

        //Act
        var result = _mapper.Map<Domain.ViewModels.Company>(dto);

        //Assert
        result.Id.Should().Be(dto.Id);
        result.Name.Should().Be(dto.Name);
        result.AddressOne.Should().Be(dto.AddressOne);
        result.AddressTwo.Should().Be(dto.AddressTwo);
        result.AddressThree.Should().Be(dto.AddressThree);
        result.Postcode.Should().Be(dto.Postcode);
        result.Country.Should().Be(dto.Country);
        result.Active.Should().Be(dto.Active);
        result.HasActiveInsurancePolicy.Should().BeFalse();
    }

    #endregion
}
