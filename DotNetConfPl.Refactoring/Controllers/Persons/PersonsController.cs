using System;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Application;
using Microsoft.AspNetCore.Mvc;

namespace DotNetConfPl.Refactoring.Controllers.Persons
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Get all persons.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var employees = await _personService.GetAllPersons();

            return Ok(employees);
        }

        /// <summary>
        /// Change Person names.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{personId}")]
        public async Task<ActionResult> Put(Guid personId, EditPersonRequest request)
        {
            await _personService.ChangePersonNames(personId, request.FirstName, request.LastName);

            return Ok();
        }

        /// <summary>
        /// Set Person as Employee of Company.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{personId}/companies/{companyId}")]
        public async Task<ActionResult> Post(Guid personId, Guid companyId, SetPersonAsEmployeeRequest request)
        {
            await _personService.SetPersonAsEmployee(personId, companyId, request.Email, request.Phone);
            
            return Ok();
        }
    }
}
