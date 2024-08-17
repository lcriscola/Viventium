using Microsoft.AspNetCore.Mvc;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Viventium.DTOs;
using Viventium.WebAPI.Controllers;

namespace Viventium.Tests.Controllers.CompanyControllers
{
    public class GetCompanyTests
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
        public async Task Null_Company_Should_Return_NotFound()
        {
            _companyService.Setup(x => x.GetCompany(1)).ReturnsAsync(()=>null);
            var action = await _controller.GetCompany(1);
            Assert.IsInstanceOf<NotFoundObjectResult>(action.Result);
            Assert.That(action.Value, Is.Null);
        }

        [Test]
        public async Task Valid_Company_Company_Should_Return_OK()
        {
            _companyService.Setup(x => x.GetCompany(1)).ReturnsAsync(() => null);

            _companyService.Setup(x => x.GetCompany(2)).ReturnsAsync(() => new DTOs.Company()
            {
                Code = "2",
                Description = "desc",
                EmployeeCount = 3,
                Employees = []
            });
            var action = await _controller.GetCompany(1);
            Assert.IsInstanceOf<NotFoundObjectResult>(action.Result);

            Assert.That(((NotFoundObjectResult)action.Result!).Value, Is.EqualTo(1));


            action = await _controller.GetCompany(2);
            Assert.IsInstanceOf<OkObjectResult>(action.Result);
            DTOs.Company value = (DTOs.Company)((OkObjectResult)action.Result!).Value!;

            Assert.That(value.Code, Is.EqualTo("2"));

        }
    }
}
