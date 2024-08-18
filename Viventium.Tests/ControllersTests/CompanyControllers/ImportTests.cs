using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Newtonsoft.Json.Linq;

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

 
        [Test]
        public async Task ImportFile_With_No_Errors_Should_Return_200()
        {
            var file = GetFile(DataHelper.GetFileContent());
            _companyService.Setup(x => x.ImportCSV(It.IsAny<Stream>())).ReturnsAsync(new List<string>());

            var action = await _controller.Import(file);

            Assert.IsInstanceOf<OkResult>(action);
        }
        [Test]
        public async Task ImportFile_With_Validation_Errors_Should_Return_400()
        {
            var file = GetFile(DataHelper.GetFileContent());
            _companyService.Setup(x => x.ImportCSV(It.IsAny<Stream>())).ReturnsAsync(new List<string>() { "some error" });

            var action = await _controller.Import(file);

            Assert.IsInstanceOf<BadRequestObjectResult>(action);
        }

        [Test]
        public async Task ImportFile_With_No_File_Should_Return_400()
        {
            _companyService.Setup(x => x.ImportCSV(It.IsAny<Stream>())).ReturnsAsync(new List<string>() { "some error" });
            var action = await _controller.Import(null);

            Assert.IsInstanceOf<BadRequestObjectResult>(action);
        }

    }
}
