using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Business.Infrastructure
{
    public interface ICompanyService
    {
        Task<List<DTOs.CompanyHeader>> GetCompanies();
        Task<List<string>> ImportCSV(Stream stream);
    }
}
