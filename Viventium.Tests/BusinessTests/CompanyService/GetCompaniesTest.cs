using Moq;
using Moq.EntityFrameworkCore;

namespace Viventium.Tests.BusinessTests.CompanyService
{
    public class GetCompaniesTest
    {

        [SetUp]
        public void Setup()
        {
            db = new Mock<Repositores.ViventiumDataContext>();
            service = new Viventium.Business.CompanyService(db.Object);


        }
        Viventium.Business.CompanyService service;
        Mock<Repositores.ViventiumDataContext> db;

        [Test]
        public async Task GetCompnies_Should_Return_A_List()
        {
            db.Setup(x => x.Companies).ReturnsDbSet(DataHelper.GetCompanies());


            var companiesDto = await service.GetCompanies();
            Assert.That(companiesDto.Length, Is.EqualTo(2));
            Assert.That(companiesDto[0].EmployeeCount, Is.EqualTo(2));
            Assert.That(companiesDto[1].EmployeeCount, Is.EqualTo(3));
        }
    }
}
