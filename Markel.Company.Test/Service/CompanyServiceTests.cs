using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Markel.Company.Repository.Interfaces;
using Markel.Company.Service;
using Moq;

namespace Markel.Company.Test.Service;

public class CompanyServiceTests
{
    private IMapper _mapper => new Mapper(new MapperConfig());
    private readonly Mock<ICompanyRepository> _mockCompanyRepository;

    public CompanyServiceTests()
    {
        _mockCompanyRepository = new Mock<ICompanyRepository>();
    }

    #region Ctor

    [Fact]
    public void Ctor_NullCompanyRepository_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new CompanyService(null, _mapper));

    [Fact]
    public void Ctor_NullMapper_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new CompanyService(_mockCompanyRepository.Object, null));

    [Fact]
    public void Ctor_DoesNotThrowException()
    {
        Exception exception = Record.Exception(GetService);

        exception.Should().BeNull();
    }

    #endregion

    #region Get

    [Theory, AutoData]
    public async Task Get_CompanyFound_ReturnsCompany(int id, Domain.Entities.Company company)
    {
        //Arrange
        _ = _mockCompanyRepository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(company);

        //Act
        var result = await GetService().Get(id);

        //Assert
        result.Should().NotBeNull();
    }

    [Theory, AutoData]
    public async Task Get_CompanyNotFound_ReturnsNull(int id)
    {
        //Arrange
        _ = _mockCompanyRepository.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync((Domain.Entities.Company)null);

        //Act
        var result = await GetService().Get(id);

        //Assert
        result.Should().BeNull();
    }

    #endregion

    #region Create

    [Theory, AutoData]
    public async void Create_ValidCompany_ReturnsCompany(Domain.Dtos.Company company)
    {
        //Arrange
        var expected = _mapper.Map<Domain.Entities.Company>(company);
        _ = _mockCompanyRepository.Setup(s => s.Create(It.IsAny<Domain.Entities.Company>())).ReturnsAsync(expected);
        
        //Act
        var result = await GetService().Create(company);

        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(expected.Name);
        result.AddressOne.Should().Be(expected.AddressOne);
        result.AddressTwo.Should().Be(expected.AddressTwo);
        result.AddressThree.Should().Be(expected.AddressThree);
        result.Postcode.Should().Be(expected.Postcode);
        result.Country.Should().Be(expected.Country);
        result.Active.Should().Be(expected.Active);
    }

    [Theory, AutoData]
    public async void Create_FailToCreate_ReturnsNull(Domain.Dtos.Company company)
    {
        //Arrange
        _ = _mockCompanyRepository.Setup(s => s.Create(It.IsAny<Domain.Entities.Company>())).ReturnsAsync((Domain.Entities.Company) null);

        //Act
        var result = await GetService().Create(company);

        //Assert
        result.Should().BeNull();
    }

    #endregion

    #region Update

    [Theory]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public async void Update_ValidCompany_ReturnsResult(bool expected, int id, Domain.Dtos.Company company)
    {
        //Arrange
        _ = _mockCompanyRepository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Domain.Entities.Company>())).ReturnsAsync(expected);

        //Act
        var result = await GetService().Update(id, company);

        //Assert
        result.Should().Be(expected);
    }

    #endregion

    private CompanyService GetService() => new(_mockCompanyRepository.Object, _mapper);
}
