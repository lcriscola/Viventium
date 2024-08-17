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
    public class GetEmployeeTests
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
        public async Task Test_NotFound()
        {
            _companyService.Setup(x => x.GetEmployee(1, "E1")).ReturnsAsync(() => null);

            var action = await _controller.GetEmployee(1, "E1");
            Assert.IsInstanceOf<NotFoundObjectResult>(action.Result);

        }

        [Test]
        public async Task Test_OK()
        {
            _companyService.Setup(x => x.GetEmployee(1, "E1")).ReturnsAsync(() => new DTOs.Employee()
            {
                Department="",
                Email="",
                EmployeeNumber="E1",
                FullName="",
                ManagerEmployeeNumber="",
                HireDate=null,
                Managers = []
            });

            var action = await _controller.GetEmployee(1, "E1");
            var result = (OkObjectResult)action.Result!;
            var value = (DTOs.Employee)result.Value!;

            Assert.That(value.EmployeeNumber, Is.EqualTo("E1"));

        }
    }
}
