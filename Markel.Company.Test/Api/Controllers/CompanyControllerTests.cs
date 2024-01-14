using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Markel.Company.Api.Controllers;
using Markel.Company.Service;
using Markel.Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace Markel.Company.Test.Api.Controllers;

public class CompanyControllerTests
{
    private readonly Mock<ICompanyService> _mockCompanyService;
    private readonly Mock<ILogger<CompanyController>> _loggerMock;
    private IMapper _mapper => new Mapper(new MapperConfig());

    public CompanyControllerTests()
    {
        _mockCompanyService = new();
        _loggerMock = new();
    }

    #region Ctor

    [Fact]
    public void Ctor_NullLogger_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new CompanyController(null, _mockCompanyService.Object, _mapper));

    [Fact]
    public void Ctor_NullCompanyService_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new CompanyController(_loggerMock.Object, null, _mapper));

    [Fact]
    public void Ctor_NullMapper_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new CompanyController(_loggerMock.Object, _mockCompanyService.Object, null));

    [Fact]
    public void Ctor_DoesNotThrowException()
    {
        Exception exception = Record.Exception(GetController);

        exception.Should().BeNull();
    }

    #endregion

    #region Get

    [Theory, AutoData]
    public async Task Get_ValidId_ReturnsCompany(int id, Domain.Dtos.Company expected)
    {
        //Arrange
        _ = _mockCompanyService.Setup(s => s.Get(id)).ReturnsAsync(expected);

        //Act
        var result = await GetController().Get(id) as OkObjectResult;
        var resultCompany = result.Value as Domain.ViewModels.Company;

        //Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        resultCompany.Name.Should().Be(expected.Name);
        resultCompany.AddressOne.Should().Be(expected.AddressOne);
        resultCompany.AddressTwo.Should().Be(expected.AddressTwo);
        resultCompany.AddressThree.Should().Be(expected.AddressThree);
        resultCompany.Postcode.Should().Be(expected.Postcode);
        resultCompany.Country.Should().Be(expected.Country);
        resultCompany.Active.Should().Be(expected.Active);
    }

    [Theory, AutoData]
    public async Task Get_CompanyNotFound_ReturnsNotFound(int id)
    {
        //Arrange
        _ = _mockCompanyService.Setup(s => s.Get(id)).ReturnsAsync((Domain.Dtos.Company?)null);

        //Act
        var result = await GetController().Get(id) as NotFoundObjectResult;

        //Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        result.Value.Should().Be($"Company with Id: {id} not found");
    }

    #endregion

    #region Post

    [Theory, AutoData]
    public async Task Post_ValidCompany_ReturnsCompany(Domain.Dtos.Company expected)
    {
        //Arrange
        _ = _mockCompanyService.Setup(s => s.Create(It.IsAny<Domain.Dtos.Company>())).ReturnsAsync(expected);
        var viewModel = _mapper.Map<Domain.ViewModels.CreateCompany>(expected);

        //Act
        var result = await GetController().Post(viewModel) as OkObjectResult;
        var resultCompany = result.Value as Domain.ViewModels.Company;

        //Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        resultCompany.Id.Should().NotBe(0);
        resultCompany.Name.Should().Be(expected.Name);
        resultCompany.AddressOne.Should().Be(expected.AddressOne);
        resultCompany.AddressTwo.Should().Be(expected.AddressTwo);
        resultCompany.AddressThree.Should().Be(expected.AddressThree);
        resultCompany.Postcode.Should().Be(expected.Postcode);
        resultCompany.Country.Should().Be(expected.Country);
        resultCompany.Active.Should().Be(expected.Active);
    }

    [Theory, AutoData]
    public async Task Post_CompanyNotFound_ReturnsNotFound(Domain.Dtos.Company dto, Domain.ViewModels.CreateCompany viewModel)
    {
        //Arrange
        _ = _mockCompanyService.Setup(s => s.Create(dto)).ReturnsAsync((Domain.Dtos.Company?)null);

        //Act
        var result = await GetController().Post(viewModel) as BadRequestObjectResult;

        //Assert
        result.Should().NotBeNull();
        result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        result.Value.Should().Be("Unable to create company.");
    }

    #endregion

    private CompanyController GetController()
        => new(_loggerMock.Object, _mockCompanyService.Object, _mapper);
}
