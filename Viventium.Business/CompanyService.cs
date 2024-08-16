using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Viventium.Models;

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
            Dictionary<CompanyEmployeeId, ImportModel> assigndManagers = new();
            Dictionary<CompanyEmployeeId, ImportModel> allEmployees = new();

            while (!sr.EndOfStream)
            {
                index++;
                string line = (await sr.ReadLineAsync())!;

                try
                {
                    if (String.IsNullOrEmpty(line))
                        continue;

                    var model = ImportModel.Parse(line);
                    model.LineNumber = index;

                    if (allEmployees.ContainsKey(new CompanyEmployeeId(model.CompanyId, model.EmployeeNumber)))
                    {
                        throw new ValidationException($"{model.CompanyId} {model.EmployeeNumber} already exists  in {model.LineNumber}");
                    }

                    allEmployees.Add(new CompanyEmployeeId(model.CompanyId, model.EmployeeNumber), model);



                    //Assuming employees with no manager are managers.
                    if (model.ManagerEmployeeNumber is not null)
                    {
                        assigndManagers[new (model.CompanyId, model.ManagerEmployeeNumber)] = model;
                    }

                }
                catch (ValidationException ex)
                {
                    errors.Add($"Line {index}. {ex.Message}");
                }

            }

            //make sure that the assigned managers are real employees
            foreach (var mgr in assigndManagers)
            {
                var companyId = mgr.Key.CompanyId;
                var managerId = mgr.Key.EmployeeId;

                if (!allEmployees.TryGetValue(new CompanyEmployeeId(companyId, managerId), out var employee))
                {
                    errors.Add($"Line {mgr.Value.LineNumber}. Manager {managerId} was not found as an employee in Company Id {companyId}");
                }
            }

            //now we can do the saving


            return errors;
        }
    }
}
