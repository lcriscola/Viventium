using Moq;
using Moq.EntityFrameworkCore;

namespace Viventium.Tests.BusinessTests.CompanyService
{
    public class GetCompanyTest
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
        public async Task Found_Company_Should_Return_A_Company()
        {
            db.Setup(x => x.Companies).ReturnsDbSet(DataHelper.GetCompanies());


            var companyDto = await service.GetCompany(1);
            Assert.That(companyDto!.Id, Is.EqualTo(1));
        }


        [Test]
        public async Task NotFound_Company_Should_Return_Null()
        {
            db.Setup(x => x.Companies).ReturnsDbSet(DataHelper.GetCompanies());


            var companyDto = await service.GetCompany(3);
            Assert.That(companyDto, Is.Null);

        }
    }
}
