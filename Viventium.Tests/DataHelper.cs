using Viventium.Models.DB;

namespace Viventium.Tests
{
    public static class DataHelper
    {
        public static Models.DB.Company[] GetCompanies()
        {
            var companies = new Models.DB.Company[]
            {
                new Models.DB.Company()
                {
                    Code="code1",
                    CompanyId=1,
                    Description="desc1",
                    Employees=[
                        new Models.DB.Employee() {
                        Department="dep",
                        Email="email",
                        EmployeeNumber="E1",
                        FirstName="first_name",
                        LastName="last_name",
                        CompanyId=1
                    },
                        new Models.DB.Employee() {
                        Department="dep",
                        Email="email",
                        EmployeeNumber="E2",
                        FirstName="first_name",
                        LastName="last_name",
                        CompanyId=1

                    },
                        ]
                },
                new Models.DB.Company()
                {
                    Code="code2",
                    CompanyId=2,
                    Description="desc",
                    Employees=[
                        new Models.DB.Employee() {
                        Department="dep",
                        Email="email",
                        EmployeeNumber="E1",
                        FirstName="first_name",
                        LastName="last_name",
                        CompanyId=2
                    },
                        new Models.DB.Employee() {
                        Department="dep",
                        Email="email",
                        EmployeeNumber="E2",
                        FirstName="first_name",
                        LastName="last_name",
                        CompanyId=2
                    },
                        new Models.DB.Employee() {
                        Department="dep",
                        Email="email",
                        EmployeeNumber="E3",
                        FirstName="first_name",
                        LastName="last_name",
                        CompanyId=2
                    }
                        ]
                },

            };
            return companies;

        }

        internal static IEnumerable<Employee> GetEmployees()
        {
            return
            [
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E1",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S1"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E2",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S1"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E3",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S2"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E4",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S2"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E5",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S3"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E6",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S3"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E7",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S4"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E8",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="S4"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="S1",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="M1"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="S2",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="M1"
                },
                  new Employee()
                  {
                      CompanyId=2,
                      Department="Dep",
                      Email="email",
                      EmployeeNumber="S3",
                      FirstName="first_name_2",
                      LastName="last_name_2",
                    ManagerEmployeeNumber="M2"
                  },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="S4",
                    FirstName="first_name",
                    LastName="last_name",
                    ManagerEmployeeNumber="M2"
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="M1",
                    FirstName="first_name",
                    LastName="last_name",
                },
                new Employee()
                {
                    CompanyId=1,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="M2",
                    FirstName="first_name",
                    LastName="last_name",
                },
                                new Employee()
                {
                    CompanyId=2,
                    Department="Dep",
                    Email="email",
                    EmployeeNumber="E1",
                    FirstName="first_name",
                    LastName="last_name",
                },
            ];
        }
    }
}
