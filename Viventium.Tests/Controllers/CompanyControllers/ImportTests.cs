using Microsoft.AspNetCore.Http;
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
    public class ImportTests
    {
        [SetUp]
        public void Setup()
        {
            _companyService = new Moq.Mock<Business.Infrastructure.ICompanyService>();
            _controller = new CompanyController(_companyService.Object);
        }
        WebAPI.Controllers.CompanyController _controller;
        Mock<Business.Infrastructure.ICompanyService> _companyService;


        FormFile GetFile(string text)
        {
            var st = new MemoryStream(Encoding.UTF8.GetBytes(text));

            FormFile file = new FormFile(st, 0, st.Length, "test", "test.csv");
            st.Seek(0, SeekOrigin.Begin);
            return file;
        }

        const string EmployeeHierarchyText = """
            CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
            1,Whiskey,Whiskey Description,E1,Free,Alderman,falderman0@dot.gov,Accounting,,S1
            1,Whiskey,Whiskey Description,E2,Free,Alderman,falderman0@dot.gov,Accounting,,S1
            1,Whiskey,Whiskey Description,E3,Free,Alderman,falderman0@dot.gov,Accounting,,S2
            1,Whiskey,Whiskey Description,E4,Free,Alderman,falderman0@dot.gov,Accounting,,S2
            1,Whiskey,Whiskey Description,E5,Free,Alderman,falderman0@dot.gov,Accounting,,S3
            1,Whiskey,Whiskey Description,E6,Free,Alderman,falderman0@dot.gov,Accounting,,S3
            1,Whiskey,Whiskey Description,E7,Free,Alderman,falderman0@dot.gov,Accounting,,S4
            1,Whiskey,Whiskey Description,E8,Free,Alderman,falderman0@dot.gov,Accounting,,S4
            1,Whiskey,Whiskey Description,S1,Free,Alderman,falderman0@dot.gov,Accounting,,M1
            1,Whiskey,Whiskey Description,S2,Free,Alderman,falderman0@dot.gov,Accounting,,M1
            1,Whiskey,Whiskey Description,S3,Free,Alderman,falderman0@dot.gov,Accounting,,M2
            1,Whiskey,Whiskey Description,S4,Free,Alderman,falderman0@dot.gov,Accounting,,M2
            1,Whiskey,Whiskey Description,M1,Free,Alderman,falderman0@dot.gov,Accounting,,
            1,Whiskey,Whiskey Description,M2,Free,Alderman,falderman0@dot.gov,Accounting,,
            """;

        [Test]
        public async Task ImportFile_With_No_Errors()
        {
            var file = GetFile(EmployeeHierarchyText);
            _companyService.Setup(x => x.ImportCSV(It.IsAny<Stream>())).ReturnsAsync(new List<string>());

            var action = await _controller.Import(file);

            Assert.IsInstanceOf<OkResult>(action);
        }
        [Test]
        public async Task ImportFile_With_Validation_Errors()
        {
            var file = GetFile(EmployeeHierarchyText);
            _companyService.Setup(x => x.ImportCSV(It.IsAny<Stream>())).ReturnsAsync(new List<string>() { "some error" });

            var action = await _controller.Import(file);

            Assert.IsInstanceOf<BadRequestObjectResult>(action);
        }

        [Test]
        public async Task ImportFile_With_No_File_Errors()
        {
            _companyService.Setup(x => x.ImportCSV(It.IsAny<Stream>())).ReturnsAsync(new List<string>() { "some error" });
            var action = await _controller.Import(null);

            Assert.IsInstanceOf<BadRequestObjectResult>(action);
        }

    }
}
