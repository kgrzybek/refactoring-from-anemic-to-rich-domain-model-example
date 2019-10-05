using System;
using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Application;
using Microsoft.AspNetCore.Mvc;

namespace DotNetConfPl.Refactoring.Controllers.Companies
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(
            ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Get all Companies.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var companies = await _companyService.GetAllCompanies();

            return Ok(companies);
        }
        
        /// <summary>
        /// Create Company.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(AddCompanyRequest request)
        {
            await _companyService.CreateCompany(request.Name);

            return Ok();
        }

        /// <summary>
        /// Edit Company.
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{companyId}")]
        public async Task<ActionResult> Put(Guid companyId, EditCompanyRequest request)
        {
            await _companyService.EditCompany(companyId, request.Name);

            return Ok();
        }
    }
}
