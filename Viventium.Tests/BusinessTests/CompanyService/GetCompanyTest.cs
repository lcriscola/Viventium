using Moq;

namespace Viventium.Tests.BusinessTests.CompanyService
{
    public class GetCompanyTest
    {

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<Repositores.Infrastructure.IGenericRepository>();
            _service = new Viventium.Business.CompanyService(_repo.Object);


        }
        Viventium.Business.CompanyService _service;
        Mock<Repositores.Infrastructure.IGenericRepository> _repo;

        [Test]
        public async Task Found_Company_Should_Return_A_Company()
        {
            _repo.Setup(x => x.Set<Models.DB.Company>()).Returns(DataHelper.AsDBSet(DataHelper.GetCompanies()));


            var companyDto = await _service.GetCompany(1);
            Assert.That(companyDto!.Id, Is.EqualTo(1));
        }


        [Test]
        public async Task NotFound_Company_Should_Return_Null()
        {

            _repo.Setup(x => x.Set<Models.DB.Company>()).Returns(DataHelper.AsDBSet(DataHelper.GetCompanies()));

            var companyDto = await _service.GetCompany(3);
            Assert.That(companyDto, Is.Null);

        }
    }
}
