using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.DTOs
{
    public class Company : CompanyHeader
    {
       public required EmployeeHeader[] Employees { get; set; } // List of EmployeeHeader objects in the given company
    }
}
