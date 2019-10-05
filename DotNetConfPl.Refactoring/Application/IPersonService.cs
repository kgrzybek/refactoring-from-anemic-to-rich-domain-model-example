using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Controllers;
using DotNetConfPl.Refactoring.Controllers.Persons;

namespace DotNetConfPl.Refactoring.Application
{
    public interface IPersonService
    {
        Task<List<PersonDto>> GetAllPersons();

        Task ChangePersonNames(Guid personId, string firstName, string lastName);

        Task SetPersonAsEmployee(Guid personId, Guid companyId, string email, string phone);
    }
}