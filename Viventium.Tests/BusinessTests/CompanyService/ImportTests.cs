using Moq;
using Moq.EntityFrameworkCore;

using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
            var options = new DbContextOptionsBuilder<Repositores.ViventiumDataContext>()
                .UseInMemoryDatabase("ViventiumDataContext")
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            db = new Viventium.Repositores.ViventiumDataContext(options);
            service = new Viventium.Business.CompanyService(db);
        }
        Viventium.Business.CompanyService service;
        Repositores.ViventiumDataContext db;

        [Test]
        public async Task Import_With_Valid_Data_Shoud_Return_No_Errors()
        {
            using var st = new MemoryStream(Encoding.UTF8.GetBytes(DataHelper.GetFileContent()));
            var employees = await service.ImportCSV(st);
            Assert.That(employees.Count, Is.EqualTo(0));
        }

    }
}
