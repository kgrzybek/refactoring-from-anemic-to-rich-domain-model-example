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

            builder.HasOne(x => x.ContactEmployee).WithMany().HasForeignKey(x => x.ContactEmployeeId);
        }
    }
}