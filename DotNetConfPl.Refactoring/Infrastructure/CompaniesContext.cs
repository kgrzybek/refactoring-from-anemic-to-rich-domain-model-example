using DotNetConfPl.Refactoring.Controllers.Companies;
using DotNetConfPl.Refactoring.Controllers.Employees;
using DotNetConfPl.Refactoring.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetConfPl.Refactoring.Infrastructure
{
    public class CompaniesContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyDto> CompanyDtos { get; set; }
        public DbSet<EmployeeDto> EmployeeDtos { get; set; }

        public DbSet<Person> Persons { get; set; }

        public CompaniesContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new CompanyDtoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeDtoEntityTypeConfiguration());
        }
    }
}