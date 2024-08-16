using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Import()
        {
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
