using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Markel.Company.Repository.Interfaces;
using Markel.Company.Service;
using Moq;

namespace Markel.Company.Test.Service;

public class ClaimServiceTests
{
    private IMapper _mapper => new Mapper(new MapperConfig());
    private readonly Mock<IClaimRepository> _mockClaimRepository;

    public ClaimServiceTests()
    {
        _mockClaimRepository = new Mock<IClaimRepository>();
    }

    #region Ctor

    [Fact]
    public void Ctor_NullClaimRepository_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new ClaimService(null, _mapper));

    [Fact]
    public void Ctor_NullMapper_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new ClaimService(_mockClaimRepository.Object, null));

    [Fact]
    public void Ctor_DoesNotThrowException()
    {
        Exception exception = Record.Exception(GetService);

        exception.Should().BeNull();
    }

    #endregion

    #region Get

    [Theory, AutoData]
    public async Task Get_ClaimFound_ReturnsClaim(Guid ucr, Domain.Entities.Claim claim)
    {
        //Arrange
        _ = _mockClaimRepository.Setup(s => s.Get(It.IsAny<Guid>())).ReturnsAsync(claim);

        //Act
        var result = await GetService().Get(ucr);

        //Assert
        result.Should().NotBeNull();
    }

    [Theory, AutoData]
    public async Task Get_ClaimNotFound_ReturnsNull(Guid ucr)
    {
        //Arrange
        _ = _mockClaimRepository.Setup(s => s.Get(It.IsAny<Guid>())).ReturnsAsync((Domain.Entities.Claim)null);

        //Act
        var result = await GetService().Get(ucr);

        //Assert
        result.Should().BeNull();
    }

    #endregion

    #region Create

    [Theory, AutoData]
    public async void Create_ValidClaim_ReturnsClaim(Domain.Dtos.Claim claim)
    {
        //Arrange
        var expected = _mapper.Map<Domain.Entities.Claim>(claim);
        _ = _mockClaimRepository.Setup(s => s.Create(It.IsAny<Domain.Entities.Claim>())).ReturnsAsync(expected);

        //Act
        var result = await GetService().Create(claim);

        //Assert
        result.Should().NotBeNull();
        result.CompanyId.Should().Be(expected.CompanyId);
        result.ClaimDate.Should().Be(expected.ClaimDate);
        result.LossDate.Should().Be(expected.LossDate);
        result.AssuredName.Should().Be(expected.AssuredName);
        result.IncurredLoss.Should().Be(expected.IncurredLoss);
        result.Closed.Should().Be(expected.Closed);
    }

    [Theory, AutoData]
    public async void Create_FailToCreate_ReturnsNull(Domain.Dtos.Claim claim)
    {
        //Arrange
        _ = _mockClaimRepository.Setup(s => s.Create(It.IsAny<Domain.Entities.Claim>())).ReturnsAsync((Domain.Entities.Claim)null);

        //Act
        var result = await GetService().Create(claim);

        //Assert
        result.Should().BeNull();
    }

    #endregion

    #region Update

    [Theory]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public async void Update_ValidClaim_ReturnsResult(bool expected, Guid ucr, Domain.Dtos.Claim claim)
    {
        //Arrange
        _ = _mockClaimRepository.Setup(s => s.Update(It.IsAny<Guid>(), It.IsAny<Domain.Entities.Claim>())).ReturnsAsync(expected);

        //Act
        var result = await GetService().Update(ucr, claim);

        //Assert
        result.Should().Be(expected);
    }

    #endregion

    #region GetAllForCompany

    [Theory, AutoData]
    public async Task GetAllForCompany_ClaimsFound_ReturnsList(int companyId, IEnumerable<Domain.Entities.Claim> claims)
    {
        //Arrange
        _ = _mockClaimRepository.Setup(s => s.GetAllForCompany(companyId)).ReturnsAsync(claims);

        //Act
        var result = await GetService().GetAllForCompany(companyId);

        //Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(claims.Count());
    }

    [Theory, AutoData]
    public async Task GetAllForCompany_NoClaimsFound_ReturnsEmptyList(int companyId)
    {
        //Arrange
        _ = _mockClaimRepository.Setup(s => s.GetAllForCompany(companyId)).ReturnsAsync(new List<Domain.Entities.Claim>());

        //Act
        var result = await GetService().GetAllForCompany(companyId);

        //Assert
        result.Count().Should().Be(0);
    }

    #endregion


    private ClaimService GetService() => new(_mockClaimRepository.Object, _mapper);
}
