using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.DTOs
{
    public class EmployeeHeader
    {
        public required String EmployeeNumber {get;set;} // EmployeeNumber
        public required  String FullName { get; set; } // "{EmployeeFirstName} {EmployeeLastName}"
    }
}
