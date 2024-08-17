using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Models.DB
{
    [Table("Employees")]
    public  class Employee
    {


        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public required string EmployeeNumber { get; set; }


        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Department { get; set; }
        public DateTime? HireDate { get; set; }

        //[ForeignKey(nameof(Manager))]


        public string? ManagerEmployeeNumber { get; set; }

        [ForeignKey($"{nameof(CompanyId)},{nameof(ManagerEmployeeNumber)}")]
        public Employee? Manager   { get; set; }

    }
}
