using DotNetConfPl.Refactoring.Controllers.Companies;
using DotNetConfPl.Refactoring.Controllers.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetConfPl.Refactoring.Infrastructure
{
    public class EmployeeDtoEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeDto>
    {
        public void Configure(EntityTypeBuilder<EmployeeDto> builder)
        {
            builder.ToTable("v_Employees", "companies");

            builder.HasKey(b => b.Id);
        }
    }
}