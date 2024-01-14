using FluentAssertions;
using Markel.Company.Repository;

namespace Markel.Company.Test.Repository;

public class ClaimRepositoryTests
{
    private static CompanyContext _companyContext => new CompanyContext();

    #region Ctor

    [Fact]
    public void Ctor_CompanyContext_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new ClaimRepository(null));

    [Fact]
    public void Ctor_DoesNotThrowException()
    {
        Exception exception = Record.Exception(GetRepository);

        exception.Should().BeNull();
    }

    #endregion

    private ClaimRepository GetRepository()
        => new(_companyContext);
}
