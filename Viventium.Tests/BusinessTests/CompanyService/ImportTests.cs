using Moq;

using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace Viventium.Tests.BusinessTests.CompanyService
{
    /// <summary>
    /// In tnhis test I am using the InMemory provider instead of faking the context since the code is using transactions
    /// With a pure Repository Pattern (instead of relying on EF) I shoud not do this.
    /// </summary>
    public class ImportTests
    {

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<Repositores.Infrastructure.IGenericRepository>();
            _repo.Setup(x => x.Set<Models.DB.Employee>()).Returns(DataHelper.AsDBSet(new Models.DB.Employee[] { }));
            _repo.Setup(x => x.Set<Models.DB.Company>()).Returns(DataHelper.AsDBSet(new Models.DB.Company[] { }));

            _transaction = new Mock<IDbContextTransaction>();
            _repo.Setup(x => x.BeginTransactionAsync()).ReturnsAsync(_transaction.Object);

            _service = new Viventium.Business.CompanyService(_repo.Object);
        }
        Viventium.Business.CompanyService _service;
        Mock<Repositores.Infrastructure.IGenericRepository> _repo;
        Mock<IDbContextTransaction> _transaction;


        [Test]
        public async Task Import_With_Valid_Data_Shoud_Return_No_Errors()
        {
            using var st = new MemoryStream(Encoding.UTF8.GetBytes(DataHelper.GetFileContent()));



            var errors = await _service.ImportCSV(st);
            Assert.That(errors.Count, Is.EqualTo(0));

            _repo.Verify(x => x.Add(It.Is<Models.DB.Company>(c=> c.CompanyId ==1)), Times.Once());
            _repo.Verify(x => x.Add(It.Is<Models.DB.Company>(c=> c.CompanyId ==2)), Times.Never());
            _repo.Verify(x => x.Add(It.Is<Models.DB.Employee>(c=> c.CompanyId ==1 && c.EmployeeNumber=="E1")), Times.Once());
            _repo.Verify(x => x.ExecuteDeleteAsync< Models.DB.Company>());
            _repo.Verify(x => x.ExecuteDeleteAsync< Models.DB.Employee>());
            _repo.Verify(x => x.SaveChangesAsync());
        }

        [Test]
        public async Task Import_With_Duplicates_Employee_Shoud_Return_Errors()
        {
            using var st = new MemoryStream(Encoding.UTF8.GetBytes(
                """
                  CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
                  1,Whiskey,Whiskey Description,E1,Free,Alderman,falderman0@dot.gov,Accounting,,S1
                  1,Whiskey,Whiskey Description,E2,Free,Alderman,falderman0@dot.gov,Accounting,,S1
                  1,Whiskey,Whiskey Description,E1,Free,Alderman,falderman0@dot.gov,Accounting,,S1
                  1,Whiskey,Whiskey Description,E1,Free,Alderman,falderman0@dot.gov,Accounting,,S4
                  1,Whiskey,Whiskey Description,S1,Free,Alderman,falderman0@dot.gov,Accounting,,
                  2,Whiskey,Whiskey Description,E1,Free,Alderman,falderman0@dot.gov,Accounting,,S1
                  2,Whiskey,Whiskey Description,E2,Free,Alderman,falderman0@dot.gov,Accounting,,S1
                  2,Whiskey,Whiskey Description,S1,Free,Alderman,falderman0@dot.gov,Accounting,,
                  """
                ));
       


            var errors = await _service.ImportCSV(st);
            Assert.That(errors.Count, Is.EqualTo(2));
            Assert.That(errors[0].Contains("Line 3"), Is.True, errors[0]);
            Assert.That(errors[1].Contains("Line 4"), Is.True, errors[1]);

        }

    }
}
