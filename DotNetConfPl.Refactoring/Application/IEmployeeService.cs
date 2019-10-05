using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Controllers.Employees;

namespace DotNetConfPl.Refactoring.Application
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployees(Guid companyId);

        Task AddEmployee(Guid companyId, string firstName, string lastName, string phone, string email,
            bool isContactEmployee);

        Task ChangeEmployeeContact(Guid companyId, Guid employeeId, string phone, string email);

        Task DeactivateEmployee(Guid companyId, Guid employeeId);
    }
}