using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

using Swashbuckle.AspNetCore.Annotations;

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
        /// Accepts the CSV data and replaces (clears and imports) the data in the store with the provided one.
        /// All rows in the CSV data must be valid for the import to succeed. If there is at lease one error, No data will be Deletred/Inserted.
        /// </summary>
        /// <returns></returns>
        [HttpPost("/dataStore")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Some validation error was found.")]
        [SwaggerResponse(500,"Unhandled exception")]

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
        /// Returns the list of CompanyHeader objects
        /// </summary>
        /// <returns></returns>
        [HttpGet("/companies")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Some validation error was found.")]
        [SwaggerResponse(500, "Unhandled exception")]
        public async Task<ActionResult<List<DTOs.CompanyHeader>>> GetCompanies()
        {
            var data = await _companyService.GetCompanies();
            return this.Ok(data);
        }

        /// <summary>
        /// Returns the Company object by provided ID
        /// </summary>
        /// <param name="companyId">The company Id</param>
        /// <returns></returns>
        [HttpGet("/companies/{companyId:int}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Some validation error was found.")]
        [SwaggerResponse(500, "Unhandled exception")]
        public ActionResult<DTOs.Company> GetCompany(int companyId)
        {
            return this.Ok();
        }

        /// <summary>
        /// Returns the Employee object by provided company ID and employee number
        /// </summary>
        /// <param name="companyId">The company Id</param>
        /// <param name="employeeNumber">The employee number</param>
        /// <returns></returns>
        [SwaggerResponse(200)]
        [SwaggerResponse(400, "Some validation error was found.")]
        [SwaggerResponse(500, "Unhandled exception")]
        [HttpGet("/companies/{companyId}/employees/{employeeNumber}")]
        public ActionResult<DTOs.Employee> GetEmployee(int companyId, string employeeNumber)
        {
            return this.Ok();
        }
    }
}
