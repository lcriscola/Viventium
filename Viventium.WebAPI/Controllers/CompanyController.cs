using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

using Swashbuckle.AspNetCore.Annotations;

using System;
using System.IO;

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
        /// Accepts the CSV data as a STREAM (Binary file) and replaces (clears and imports) the data in the store with the provided one.
        /// All rows in the CSV data must be valid for the import to succeed. If there is at lease one error, No data will be Deletred/Inserted.
        /// </summary>
        /// <returns></returns>
        [HttpPost("/dataStore/stream")]
        [RequestSizeLimit(200_000_000)]
        public async Task<ActionResult> ImportUsingStream()
        {
             var errors = await _companyService.ImportCSV(Request.Body);
            if (errors.Count > 0)
            {
                return this.BadRequest(errors);
            }
            return this.Ok();
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
        [RequestSizeLimit(200_000_000)]

        public async Task<ActionResult> Import(IFormFile? fileData)
        {
            if (fileData is null)
                return this.BadRequest("No file was sent.");

            using var stream = fileData.OpenReadStream();
            var errors = await _companyService.ImportCSV(stream);
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
        [SwaggerResponse(500, "Unhandled exception")]
        public async Task<ActionResult<DTOs.CompanyHeader[]>> GetCompanies()
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
        [SwaggerResponse(404, "Company Id not found.")]
        [SwaggerResponse(500, "Unhandled exception")]
        public async Task<ActionResult<DTOs.Company>> GetCompany(int companyId)
        {
            var data = await _companyService.GetCompany(companyId);
            if (data is null)
                return this.NotFound(companyId);
             
            return this.Ok(data);
        }

        /// <summary>
        /// Returns the Employee object by provided company ID and employee number
        /// </summary>
        /// <param name="companyId">The company Id</param>
        /// <param name="employeeNumber">The employee number</param>
        /// <returns></returns>
        [SwaggerResponse(200)]
        [SwaggerResponse(404, "Company id or EmployeeId not found.")]
        [SwaggerResponse(500, "Unhandled exception")]
        [HttpGet("/companies/{companyId:int}/employees/{employeeNumber}")]
        public async Task<ActionResult<DTOs.Employee>> GetEmployee(int companyId, string employeeNumber)
        {

            var data = await _companyService.GetEmployee(companyId, employeeNumber);
            if (data is null)
                return this.NotFound(companyId);

            return this.Ok(data);
        }
    }
}
