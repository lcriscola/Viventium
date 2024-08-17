using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Models
{
    public record EmployeeNumberFullNameManager(string EmployeeNumber, string FullName, string? ManagerEmployeeNumber);
}
