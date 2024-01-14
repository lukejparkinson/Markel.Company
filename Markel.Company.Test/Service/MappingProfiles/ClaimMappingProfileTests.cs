using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Markel.Company.Service;

namespace Markel.Company.Test.Repository;

public class ClaimMappingProfileTests
{
    private static IMapper _mapper => new Mapper(new MapperConfig());

    #region Dto to ViewModel

    [Theory, AutoData]
    public void Map_Dto_To_ViewModel_ReturnsValid(Domain.Dtos.Claim dto)
    {
        //Arrange
        dto.ClaimDate = DateTime.UtcNow.AddDays(-1);

        //Act
        var result = _mapper.Map<Domain.ViewModels.Claim>(dto);

        //Assert
        result.Ucr.Should().Be(dto.Ucr);
        result.CompanyId.Should().Be(dto.CompanyId);
        result.ClaimDate.Should().Be(dto.ClaimDate);
        result.LossDate.Should().Be(dto.LossDate);
        result.AssuredName.Should().Be(dto.AssuredName);
        result.IncurredLoss.Should().Be(dto.IncurredLoss);
        result.Closed.Should().Be(dto.Closed);
        Math.Round(result.ClaimAgeDays).Should().Be(1);
    }

    #endregion
}
