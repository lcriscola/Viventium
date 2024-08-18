using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Viventium.DTOs;

namespace Viventium.Business.Infrastructure
{
    public interface ICompanyService
    {
        Task<DTOs.CompanyHeader[]> GetCompanies();
        Task<DTOs.Company?> GetCompany(int companyId);
        Task<Employee?> GetEmployee(int companyId, string employeeNumber);
        Task<List<string>> ImportCSV(Stream stream);
        Task<List<string>> ImportCSV2(Stream stream);
    }
}
