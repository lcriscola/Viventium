using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Viventium.Models
{
    public record ImportModel
    {
        public required int CompanyId { get; set; }
        public required string CompanyCode { get; set; }
        public required string CompanyDescription { get; set; }
        public required string EmployeeNumber { get; set; }
        public required string EmployeeFirstName { get; set; }
        public required string EmployeeLastName { get; set; }
        public required string EmployeeEmail { get; set; }
        public required string EmployeeDepartment { get; set; }
        public  DateTime? HireDate { get; set; }
        public string? ManagerEmployeeNumber { get; set; }
        public int LineNumber { get; set; }

        public static ImportModel Parse(string line)
        {
            var parts = line.Split(',', StringSplitOptions.TrimEntries).Select(x=> x.Trim()).ToArray();

            if (parts.Length != 10)
            {
                throw new ValidationException($"Contains {parts.Length}/10 columns.");
            }


            if (!int.TryParse(parts[0], out var companyId))
                throw new ValidationException($"{parts[0]} is not a number");

            DateTime hireDate = DateTime.MinValue;
            if (!String.IsNullOrEmpty(parts[8]) &&  !DateTime.TryParse(parts[8], out hireDate))
                throw new ValidationException($"{parts[8]} is not a date");

            return new ImportModel()
            {
                CompanyId = companyId,
                CompanyCode = parts[1],
                CompanyDescription = parts[2],
                EmployeeNumber = parts[3],
                EmployeeFirstName = parts[4],
                EmployeeLastName = parts[5],
                EmployeeEmail = parts[6],
                EmployeeDepartment = parts[7],
                HireDate = (hireDate == DateTime.MinValue ? null : hireDate),
                ManagerEmployeeNumber = String.IsNullOrEmpty(parts[9]) ? null : parts[9],
            };
        }

    }
}
