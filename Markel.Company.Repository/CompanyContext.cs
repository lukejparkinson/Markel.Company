using Microsoft.EntityFrameworkCore;

namespace Markel.Company.Repository;

public class CompanyContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "CompanyDb");
    }

    public DbSet<Domain.Entities.Company> Companies { get; set; }
    public DbSet<Domain.Entities.Claim> Claims { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entities.Company>().HasKey("Id");
        modelBuilder.Entity<Domain.Entities.Claim>().HasKey("Ucr");
    }
}
