using Moq;
using Moq.EntityFrameworkCore;

namespace Viventium.Tests.BusinessTests.CompanyService
{
    public class ImportTests
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
        public async Task Invalid_Company_Should_Return_Null()
        {
            db.Setup(x => x.Employees).ReturnsDbSet(DataHelper.GetEmployees());

            var employees = await service.GetEmployee(3,"E1");
            Assert.That(employees, Is.Null);
        }


        [Test]
        public async Task Invalid_Employee_Should_Return_Null()
        {
            db.Setup(x => x.Employees).ReturnsDbSet(DataHelper.GetEmployees());

            var employees = await service.GetEmployee(1, "E1");
            Assert.That(employees, Is.Not.Null);

            employees = await service.GetEmployee(1, "X3");
            Assert.That(employees, Is.Null);

        }


        [Test]
        public async Task Valid_Employee_Should_Return_DTO()
        {
            db.Setup(x => x.Employees).ReturnsDbSet(DataHelper.GetEmployees());


            var employee = await service.GetEmployee(2, "E1");
            if (employee is null)
                throw new Exception("empoloyee is null");

            Assert.That(employee.FullName, Is.EqualTo("first_name last_name"));

            employee = await service.GetEmployee(1, "E1");
            if (employee is null)
               throw new Exception("empoloyee is null");

            Assert.That(employee.FullName, Is.EqualTo("first_name last_name"));
            Assert.That(employee.Managers[0].EmployeeNumber, Is.EqualTo("S1"));
            Assert.That(employee.Managers[1].EmployeeNumber, Is.EqualTo("M1"));



            employee = await service.GetEmployee(1, "M1");

            if (employee is null)
                throw new Exception("empoloyee is null");

            Assert.That(employee.Managers.Count(), Is.EqualTo(0));


        }
    }
}
