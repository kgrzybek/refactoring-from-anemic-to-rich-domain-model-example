using System.Threading.Tasks;
using DotNetConfPl.Refactoring.Application;
using Microsoft.AspNetCore.Mvc;

namespace DotNetConfPl.Refactoring.Controllers.Companies
{
    [Route("api/imported-companies")]
    [ApiController]
    public class ImportedCompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public ImportedCompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Import Company.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(ImportCompanyRequest request)
        {
            await _companyService.ImportCompany(request.Name);

            return Ok();
        }
    }
}
