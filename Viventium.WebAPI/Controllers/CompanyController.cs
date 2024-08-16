using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

using Viventium.Business.Infrastructure;

namespace Viventium.WebAPI.Controllers
{

    /// <summary>
    /// Viventium Test endpoints
    /// </summary>
    [Controller]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService">Service to handle company operations</param>
        public CompanyController(Business.Infrastructure.ICompanyService companyService)
        {
            _companyService = companyService;
        }
        /// <summary>
        /// Imports data in CSV format
        /// </summary>
        /// <returns></returns>
        [HttpPost("/dataStore")]
        public async Task<ActionResult> Import(IFormFile fileData)
        {
            if (fileData == null)
                return this.BadRequest("No file was sent.");

            var errors = await _companyService.ImportCSV(fileData.OpenReadStream());
            if (errors.Count > 0)
            {
                return this.BadRequest(errors);
            }
            return this.Ok();
        }

        /// <summary>
        /// Returns all companies ordered by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("/companies")]
        public IActionResult GetCompanies()
        {
            return this.Ok();
        }

        /// <summary>
        /// Returns a company based on its id
        /// </summary>
        /// <param name="companyId">The company Id</param>
        /// <returns></returns>
        [HttpGet("/companies/{companyId:int}")]
        public IActionResult GetCompany(int companyId)
        {
            return this.Ok();
        }

        /// <summary>
        /// Returns an employee based on the company id and the employee id.
        /// </summary>
        /// <param name="companyId">The company Id</param>
        /// <param name="employeeNumber">The employee number</param>
        /// <returns></returns>
        [HttpGet("/companies/{companyId}/employees/{employeeNumber}")]
        public IActionResult GetEmployee(int companyId, string employeeNumber)
        {
            return this.Ok();
        }
    }
}
