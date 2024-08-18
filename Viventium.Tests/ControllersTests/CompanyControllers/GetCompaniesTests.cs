using Microsoft.AspNetCore.Mvc;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Viventium.WebAPI.Controllers;

namespace Viventium.Tests.Controllers.CompanyControllers
{
    public class GetCompaniesTests
    {
        [SetUp]
        public void Setup()
        {
            _companyService = new Moq.Mock<Business.Infrastructure.ICompanyService>();
            _controller = new CompanyController(_companyService.Object);
        }
        WebAPI.Controllers.CompanyController _controller;
        Mock<Business.Infrastructure.ICompanyService> _companyService;
        
        [Test]
        public async Task A_List_Should_Return_200()
        {
            _companyService.Setup(x => x.GetCompanies()).ReturnsAsync([new DTOs.CompanyHeader() { Code="1", Description="description", EmployeeCount=1, Id=1}]);
            var action = await _controller.GetCompanies();
            var result= action.Result as OkObjectResult;
            var companies = (DTOs.CompanyHeader[])result!.Value!;

            Assert.That(result!.StatusCode, Is.EqualTo(200));
            Assert.That(companies.Length, Is.EqualTo(1));

            Assert.That(companies[0].Id, Is.EqualTo(1));
        }
    }
}
