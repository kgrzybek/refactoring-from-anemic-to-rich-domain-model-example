using DotNetConfPl.Refactoring.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNetConfPl.Refactoring.Infrastructure
{
    public class CompaniesContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Person> Persons { get; set; }

        public CompaniesContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        }
    }
}