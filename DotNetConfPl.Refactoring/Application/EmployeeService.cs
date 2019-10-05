using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Controllers;
using DotNetConfPl.Refactoring.Controllers.Employees;
using DotNetConfPl.Refactoring.Domain;
using DotNetConfPl.Refactoring.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DotNetConfPl.Refactoring.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CompaniesContext _companiesContext;

        public EmployeeService(CompaniesContext companiesContext)
        {
            _companiesContext = companiesContext;
        }

        public async Task<List<EmployeeDto>> GetAllEmployees(Guid companyId)
        {
            return await _companiesContext.EmployeeDtos.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task AddEmployee(Guid companyId, string firstName, string lastName, 
            string phone, string email, bool isContactEmployee)
        {
            var person = Person.CreateEmployeePerson(firstName, lastName);

            _companiesContext.Persons.Add(person);

            var company = await _companiesContext.Companies.SingleAsync(x => x.Id == companyId);

            company.AddEmployee(person.Id, firstName, lastName, phone, email, isContactEmployee);

            await _companiesContext.SaveChangesAsync();
        }

        public async Task ChangeEmployeeContact(Guid companyId, Guid employeeId, string phone, string email)
        {
            var company = await _companiesContext.Companies.SingleAsync(x => x.Id == companyId);

            company.ChangeEmployeeContact(employeeId, phone, email);

            await _companiesContext.SaveChangesAsync();
        }

        public async Task DeactivateEmployee(Guid companyId, Guid employeeId)
        {
            //var employee = await _companiesContext.Employees.SingleOrDefaultAsync(x => x.CompanyId == companyId && x.Id == employeeId && x.ActiveTo == null);

            //if (employee == null)
            //{
            //    throw new BusinessException("Employee cannot be deactivated more than once");
            //}

            //employee.ActiveTo = DateTime.UtcNow;

            await _companiesContext.SaveChangesAsync();
        }
    }
}