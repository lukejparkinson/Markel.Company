using AutoMapper;
using FluentAssertions;
using Markel.Company.Api.Controllers;
using Markel.Company.Service;
using Markel.Company.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Markel.Company.Test.Api.Controllers;

public class ClaimControllerTests
{
    private static Mock<IClaimService> _mockClaimService => new Mock<IClaimService>();
    private readonly Mock<ILogger<ClaimController>> _loggerMock = new Mock<ILogger<ClaimController>>();
    private static IMapper _mapper => new Mapper(new MapperConfig());

    #region Ctor

    [Fact]
    public void Ctor_NullLogger_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new ClaimController(null, _mockClaimService.Object, _mapper));

    [Fact]
    public void Ctor_NullClaimService_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new ClaimController(_loggerMock.Object, null, _mapper));

    [Fact]
    public void Ctor_NullMapper_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new ClaimController(_loggerMock.Object, _mockClaimService.Object, null));

    [Fact]
    public void Ctor_DoesNotThrowException()
    {
        Exception exception = Record.Exception(GetController);

        exception.Should().BeNull();
    }

    #endregion

    private ClaimController GetController()
        => new(_loggerMock.Object, _mockClaimService.Object, _mapper);
}
