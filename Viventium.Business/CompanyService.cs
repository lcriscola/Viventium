using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viventium.Business
{
    public class CompanyService : Infrastructure.ICompanyService
    {
        public async Task<List<string>> ImportCSV(Stream stream)
        {

            List<string> errors = new List<string>();

            using var sr = new StreamReader(stream);
            int index = 0;
            var headers = await sr.ReadLineAsync();

            while (!sr.EndOfStream)
            {
                index++;
                var line = await sr.ReadLineAsync();

                errors.Add($"Line {index}. Some error");

            }

            return errors;
        }
    }
}
