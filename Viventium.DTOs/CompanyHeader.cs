
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.DTOs
{
    public class CompanyHeader
    {
        public int Id { get; set; } // CompanyId
        public required String Code { get; set; } // CompanyCode
        public required String Description { get; set; } // CompanyDescription
        public required int EmployeeCount { get; set; } // Number of Employees in the given company
    }
}
