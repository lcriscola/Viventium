using Moq;

namespace Viventium.Tests.BusinessTests.CompanyService
{
    public class GetCompaniesTest
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
        public async Task GetCompnies_Should_Return_A_List()
        {
            _repo.Setup(x => x.Set<Models.DB.Company>()).Returns(DataHelper.AsDBSet(DataHelper.GetCompanies()));

            var companiesDto = await _service.GetCompanies();
            Assert.That(companiesDto.Length, Is.EqualTo(2));
            Assert.That(companiesDto[0].EmployeeCount, Is.EqualTo(2));
            Assert.That(companiesDto[1].EmployeeCount, Is.EqualTo(3));
        }
    }
}
