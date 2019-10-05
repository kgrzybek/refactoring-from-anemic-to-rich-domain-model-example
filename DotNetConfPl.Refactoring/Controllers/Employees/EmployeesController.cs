using System;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Application;
using Microsoft.AspNetCore.Mvc;

namespace DotNetConfPl.Refactoring.Controllers.Employees
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get Company Employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get(Guid companyId)
        {
            var employees = await _employeeService.GetAllEmployees(companyId);

            return Ok(employees);
        }

        /// <summary>
        /// Add Employee.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(Guid companyId, AddEmployeeRequest request)
        {
            await _employeeService.AddEmployee(companyId, request.FirstName, request.LastName, request.Phone,
                request.Email, request.IsContactEmployee);

            return Ok();
        }

        /// <summary>
        /// Change Employee contact.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public async Task<ActionResult> Edit(Guid companyId, Guid employeeId, EditEmployeeRequest request)
        {
            await _employeeService.ChangeEmployeeContact(companyId, employeeId, request.Phone, request.Email);

            return Ok();
        }

        /// <summary>
        /// Deactivate employee.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpPatch("{employeeId}/deactivate")]
        public async Task<ActionResult> Deactivate(Guid companyId, Guid employeeId)
        {
            await _employeeService.DeactivateEmployee(companyId, employeeId);

            return Ok();
        }
    }
}
