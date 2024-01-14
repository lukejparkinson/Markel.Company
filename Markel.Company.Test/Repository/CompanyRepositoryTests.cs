using AutoFixture.Xunit2;
using FluentAssertions;
using Markel.Company.Repository;

namespace Markel.Company.Test.Repository;

public class CompanyRepositoryTests
{
    private static CompanyContext _companyContext => new CompanyContext();

    #region Ctor

    [Fact]
    public void Ctor_CompanyContext_ThrowsException() => Assert.Throws<ArgumentNullException>(() => new CompanyRepository(null));

    [Fact]
    public void Ctor_DoesNotThrowException()
    {
        Exception exception = Record.Exception(GetRepository);

        exception.Should().BeNull();
    }

    #endregion

    #region Get

    [Theory, AutoData]
    public async Task Get_ValidCompany_ReturnsCompany(Domain.Entities.Company expected)
    {
        //Arrange
        var repository = GetRepository();

        //Act
        var created = await repository.Create(expected);
        var result = await repository.Get(created.Id);

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

    #endregion

    #region Create

    [Theory, AutoData]
    public async Task Create_ValidCompany_ReturnsCompany(Domain.Entities.Company expected)
    {
        //Arrange
        var repository = GetRepository();

        //Act
        var result = await repository.Create(expected);

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

    #endregion

    #region Update

    [Theory, AutoData]
    public async Task Update_ValidCompany_ReturnsCompany(Domain.Entities.Company company, Domain.Entities.Company updated)
    {
        //Arrange
        var repository = GetRepository();

        //Act
        var created = await repository.Create(company);
        var result = await repository.Update(created.Id, updated);
        var expected = await repository.Get(created.Id);

        //Assert
        result.Should().BeTrue();
        expected.Id.Should().Be(created.Id);
        expected.Name.Should().Be(updated.Name);
        expected.AddressOne.Should().Be(updated.AddressOne);
        expected.AddressTwo.Should().Be(updated.AddressTwo);
        expected.AddressThree.Should().Be(updated.AddressThree);
        expected.Postcode.Should().Be(updated.Postcode);
        expected.Country.Should().Be(updated.Country);
        expected.Active.Should().Be(updated.Active);
    }

    #endregion

    private CompanyRepository GetRepository()
        => new(_companyContext);
}
