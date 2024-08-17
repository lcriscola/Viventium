using System.Text.Json.Serialization;

namespace Viventium.DTOs
{
    public class Employee : EmployeeHeader
    {
        public required String Email { get; set; } // EmployeeEmail
        public required String Department { get; set; } // EmployeeDepartment
        public DateTime? HireDate { get; set; } // HireDate. Did a little change and is nullable so I can use DateTime instead of string
        public required EmployeeHeader[]  Managers { get; set; } // List of EmployeeHeaders of the managers, ordered ascending by seniority (i.e. the immediate manager first)

        
        
        //used internally
        [JsonIgnore]
        public string? ManagerEmployeeNumber { get; set; }
    }
}
