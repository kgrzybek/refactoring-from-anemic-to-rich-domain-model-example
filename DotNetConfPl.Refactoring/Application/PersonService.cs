using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Controllers;
using DotNetConfPl.Refactoring.Controllers.Persons;
using DotNetConfPl.Refactoring.Domain;
using DotNetConfPl.Refactoring.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DotNetConfPl.Refactoring.Application
{
    public class PersonService : IPersonService
    {
        private readonly CompaniesContext _companiesContext;

        public PersonService(CompaniesContext companiesContext)
        {
            _companiesContext = companiesContext;
        }

        public async Task<List<PersonDto>> GetAllPersons()
        {
            var employees = await _companiesContext.Persons
                .ToListAsync();

            return employees.Select(MapPersonToDto).ToList();
        }

        private PersonDto MapPersonToDto(Person person)
        {
            var employeeDto = new PersonDto();
            employeeDto.Id = person.Id;
            employeeDto.FirstName = person.FirstName;
            employeeDto.LastName = person.LastName;
            employeeDto.FullName = person.FullName;

            return employeeDto;
        }

        public async Task ChangePersonNames(Guid personId, string firstName, string lastName)
        {
            var person = await _companiesContext.Persons.SingleAsync(x => x.Id == personId);

            person.SetPersonNames(firstName, lastName);

            await _companiesContext.SaveChangesAsync();
        }

        public async Task SetPersonAsEmployee(Guid personId, Guid companyId, string email, string phone)
        {
            var company = await _companiesContext.Companies.SingleAsync(x => x.Id == companyId);
            var person = await _companiesContext.Persons.SingleAsync(x => x.Id == personId);

            company.AddEmployee(personId, person.FirstName, person.LastName, phone, email, false);

            await _companiesContext.SaveChangesAsync();
        }
    }
}