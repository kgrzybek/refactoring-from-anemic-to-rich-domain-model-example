using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Controllers;
using DotNetConfPl.Refactoring.Controllers.Companies;

namespace DotNetConfPl.Refactoring.Application
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetAllCompanies();

        Task CreateCompany(string name);

        Task EditCompany(Guid companyId, string name);

        Task ImportCompany(string name);
    }
}