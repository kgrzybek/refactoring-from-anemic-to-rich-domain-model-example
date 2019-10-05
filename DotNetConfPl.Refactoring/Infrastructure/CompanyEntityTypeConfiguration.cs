using DotNetConfPl.Refactoring.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetConfPl.Refactoring.Infrastructure
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies", "companies");

            builder.HasKey(b => b.Id);

            builder.OwnsMany<Employee>("_employees", employee =>
            {
                employee.ToTable("Employees", "companies");

                employee.HasKey(b =>  b.Id);

                employee.HasForeignKey("CompanyId");
            });
        }
    }
}