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
            var employees = await _companiesContext.Employees
                .Include(x => x.Person)
                .Include(x => x.Company)
                .Where(x => x.CompanyId == companyId)
                .ToListAsync();

            return employees.Select(MapEmployeeToDto).ToList();
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

            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email))
            {
                if (company.ContactEmployeeId == employeeId)
                {
                    throw new BusinessException("Contact person must have e-mail or phone provided");
                }
            }

            if (!string.IsNullOrEmpty(email))
            {
                if (await _companiesContext.Employees.AnyAsync(x => x.Id == companyId && x.Email == email && x.ActiveTo == null))
                {
                    throw new BusinessException("Employee email address must be unique in company");
                }
            }

            if (!string.IsNullOrEmpty(phone))
            {
                if (await _companiesContext.Employees.AnyAsync(x => x.Id == companyId && x.Phone == phone && x.ActiveTo == null))
                {
                    throw new BusinessException("Employee phone number must be unique in company");
                }
            }

            var employee = await _companiesContext.Employees.SingleAsync(x => x.Id == employeeId);

            employee.Email = email;
            employee.Phone = phone;

            await _companiesContext.SaveChangesAsync();
        }

        public async Task DeactivateEmployee(Guid companyId, Guid employeeId)
        {
            var employee = await _companiesContext.Employees.SingleOrDefaultAsync(x => x.CompanyId == companyId && x.Id == employeeId && x.ActiveTo == null);

            if (employee == null)
            {
                throw new BusinessException("Employee cannot be deactivated more than once");
            }

            employee.ActiveTo = DateTime.UtcNow;

            await _companiesContext.SaveChangesAsync();
        }

        private EmployeeDto MapEmployeeToDto(Employee employee)
        {
            var employeeDto = new EmployeeDto();
            employeeDto.Id = employee.Id;
            employeeDto.Email = employee.Email;
            employeeDto.Phone = employee.Phone;
            employeeDto.ActiveFrom = employee.ActiveFrom;
            employeeDto.ActiveTo = employee.ActiveTo;
            employeeDto.PersonId = employee.PersonId;
            employeeDto.CompanyName = employee.Company?.Name;
            employeeDto.FullName = employee.Person?.FullName;
            employeeDto.CompanyId = employee.CompanyId;

            return employeeDto;
        }
    }
}