using DotNetConfPl.Refactoring.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetConfPl.Refactoring.Infrastructure
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "companies");

            builder.HasKey(b =>  b.Id);

            builder.HasOne(x => x.Person).WithMany().HasForeignKey(x => x.PersonId);
            builder.HasOne(x => x.Company).WithMany().HasForeignKey(x => x.CompanyId);
        }
    }
}