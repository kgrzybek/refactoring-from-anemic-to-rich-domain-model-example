using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Controllers;
using DotNetConfPl.Refactoring.Controllers.Companies;
using DotNetConfPl.Refactoring.Domain;
using DotNetConfPl.Refactoring.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DotNetConfPl.Refactoring.Application
{
    public class CompanyService : ICompanyService
    {
        private readonly CompaniesContext _companiesContext;

        public CompanyService(CompaniesContext companiesContext)
        {
            _companiesContext = companiesContext;
        }

        public async Task<List<CompanyDto>> GetAllCompanies()
        {
            var companies = await _companiesContext.Companies.Include(x => x.ContactEmployee.Person).ToListAsync();

            return companies.Select(MapCompanyToDto).ToList();
        }

        public async Task CreateCompany(string name)
        {
            await ValidateName(name);

            var company = Company.CreateEntered(name);

            _companiesContext.Add(company);

            await _companiesContext.SaveChangesAsync();
        }

        private async Task ValidateName(string name)
        {
            if (await _companiesContext.Companies.AnyAsync(x => x.Name == name))
            {
                throw new BusinessException("Company name must be unique");
            }
        }

        public async Task EditCompany(Guid companyId, string name)
        {
            var company = await _companiesContext.Companies.SingleOrDefaultAsync(x => x.Id == companyId);

            if (company.Name == name)
            {
                return;
            }

            await ValidateName(name);

            company.Name = name;

            await _companiesContext.SaveChangesAsync();
        }

        public async Task ImportCompany(string name)
        {
            await ValidateName(name);

            var company = Company.CreateImported(name);

            _companiesContext.Add(company);

            await _companiesContext.SaveChangesAsync();
        }

        private CompanyDto MapCompanyToDto(Company company)
        {
            var companyDto = new CompanyDto();
            companyDto.Id = company.Id;
            companyDto.Name = company.Name;
            companyDto.Source = company.Source;
            companyDto.ContactEmployeeFullName = company.ContactEmployee?.Person.FullName;
            companyDto.ContactEmployeeEmail = company.ContactEmployee?.Email;
            companyDto.ContactEmployeePhone = company.ContactEmployee?.Phone;

            return companyDto;
        }
    }
}