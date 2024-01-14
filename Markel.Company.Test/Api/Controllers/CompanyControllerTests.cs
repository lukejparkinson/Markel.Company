using AutoMapper;
using FluentAssertions;
using Markel.Company.Api.Controllers;
using Markel.Company.Service;
using Markel.Company.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Markel.Company.Test.Api.Controllers;

public class CompanyControllerTests
{
    private static Mock<ICompanyService> _mockCompanyService => new Mock<ICompanyService>();
    private readonly Mock<ILogger<CompanyController>> _loggerMock = new Mock<ILogger<CompanyController>>();
    private static IMapper _mapper => new Mapper(new MapperConfig());

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

    private CompanyController GetController()
        => new(_loggerMock.Object, _mockCompanyService.Object, _mapper);
}
