namespace Viventium.DTOs
{
    public class Employee : EmployeeHeader
    {
        String Email; // EmployeeEmail
        String Department; // EmployeeDepartment
        DateTime HireDate; // HireDate
        EmployeeHeader[] Managers; // List of EmployeeHeaders of the managers, ordered ascending by seniority (i.e. the immediate manager first)
    }
}
