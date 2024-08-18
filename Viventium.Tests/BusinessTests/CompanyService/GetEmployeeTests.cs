using Moq;

namespace Viventium.Tests.BusinessTests.CompanyService
{
    public class GetEmployeeTests
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
        public async Task Invalid_Company_Should_Return_Null()
        {
            _repo.Setup(x => x.Set<Models.DB.Employee>()).Returns(DataHelper.AsDBSet(DataHelper.GetEmployees()));

            var employees = await _service.GetEmployee(3,"E1");
            Assert.That(employees, Is.Null);
        }


        [Test]
        public async Task Invalid_Employee_Should_Return_Null()
        {
            _repo.Setup(x => x.Set<Models.DB.Employee>()).Returns(DataHelper.AsDBSet(DataHelper.GetEmployees()));

            var employees = await _service.GetEmployee(1, "E1");
            Assert.That(employees, Is.Not.Null);

            employees = await _service.GetEmployee(1, "X3");
            Assert.That(employees, Is.Null);

        }


        [Test]
        public async Task Valid_Employee_Should_Return_DTO()
        {
            _repo.Setup(x => x.Set<Models.DB.Employee>()).Returns(DataHelper.AsDBSet(DataHelper.GetEmployees()));

            var employee = await _service.GetEmployee(2, "E1");
            if (employee is null)
                throw new Exception("empoloyee is null");

            Assert.That(employee.FullName, Is.EqualTo("first_name last_name"));

            employee = await _service.GetEmployee(1, "E1");
            if (employee is null)
               throw new Exception("empoloyee is null");

            Assert.That(employee.FullName, Is.EqualTo("first_name last_name"));
            Assert.That(employee.Managers[0].EmployeeNumber, Is.EqualTo("S1"));
            Assert.That(employee.Managers[1].EmployeeNumber, Is.EqualTo("M1"));



            employee = await _service.GetEmployee(1, "M1");

            if (employee is null)
                throw new Exception("empoloyee is null");

            Assert.That(employee.Managers.Count(), Is.EqualTo(0));


        }
    }
}
