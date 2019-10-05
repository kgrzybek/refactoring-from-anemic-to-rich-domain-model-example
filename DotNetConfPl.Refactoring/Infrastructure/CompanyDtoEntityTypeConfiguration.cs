using DotNetConfPl.Refactoring.Controllers.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetConfPl.Refactoring.Infrastructure
{
    public class CompanyDtoEntityTypeConfiguration : IEntityTypeConfiguration<CompanyDto>
    {
        public void Configure(EntityTypeBuilder<CompanyDto> builder)
        {
            builder.ToTable("v_Companies", "companies");

            builder.HasKey(b => b.Id);
        }
    }
}