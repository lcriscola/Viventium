using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.DTOs
{
    public class Company : CompanyHeader
    {
        EmployeeHeader[] Employees; // List of EmployeeHeader objects in the given company
    }
}
