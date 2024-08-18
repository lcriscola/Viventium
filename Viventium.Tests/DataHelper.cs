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

        public static string GetFileContent()
        {
            return  
                """"
                CompanyId,CompanyCode,CompanyDescription,EmployeeNumber,EmployeeFirstName,EmployeeLastName,EmployeeEmail,EmployeeDepartment,HireDate,ManagerEmployeeNumber
                1,Whiskey,Whiskey Description,E1,Free,Alderman,falderman0@dot.gov,Accounting,,S1
                1,Whiskey,Whiskey Description,E2,Free,Alderman,falderman0@dot.gov,Accounting,,S1
                1,Whiskey,Whiskey Description,E3,Free,Alderman,falderman0@dot.gov,Accounting,,S2
                1,Whiskey,Whiskey Description,E4,Free,Alderman,falderman0@dot.gov,Accounting,,S2
                1,Whiskey,Whiskey Description,E5,Free,Alderman,falderman0@dot.gov,Accounting,,S3
                1,Whiskey,Whiskey Description,E6,Free,Alderman,falderman0@dot.gov,Accounting,,S3
                1,Whiskey,Whiskey Description,E7,Free,Alderman,falderman0@dot.gov,Accounting,,S4
                1,Whiskey,Whiskey Description,E8,Free,Alderman,falderman0@dot.gov,Accounting,,S4
                1,Whiskey,Whiskey Description,S1,Free,Alderman,falderman0@dot.gov,Accounting,,M1
                1,Whiskey,Whiskey Description,S2,Free,Alderman,falderman0@dot.gov,Accounting,,M1
                1,Whiskey,Whiskey Description,S3,Free,Alderman,falderman0@dot.gov,Accounting,,M2
                1,Whiskey,Whiskey Description,S4,Free,Alderman,falderman0@dot.gov,Accounting,,M2
                1,Whiskey,Whiskey Description,M1,Free,Alderman,falderman0@dot.gov,Accounting,,
                1,Whiskey,Whiskey Description,M2,Free,Alderman,falderman0@dot.gov,Accounting,,
                """";
        }
    }
}
