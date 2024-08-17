using Microsoft.EntityFrameworkCore;

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using Viventium.Models;
using Viventium.Repositores;

namespace Viventium.Business
{
    public class CompanyService : Infrastructure.ICompanyService
    {
        private readonly ViventiumDataContext _db;

        public CompanyService(Repositores.ViventiumDataContext db)
        {
            _db = db;
        }
        public async Task<List<string>> ImportCSV(Stream stream)
        {

            List<string> errors = new List<string>();

            using var sr = new StreamReader(stream);
            int index = 0;
            Dictionary<CompanyEmployeeId, ImportModel> assignedManagers = new();
            Dictionary<CompanyEmployeeId, ImportModel> allEmployees = new();


            //skip first line
            var headers = await sr.ReadLineAsync();

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




                    //store the manager so I can check them for valid employee once all employees are parsed.
                    if (model.ManagerEmployeeNumber is not null)
                    {
                        assignedManagers[new (model.CompanyId, model.ManagerEmployeeNumber)] = model;
                    }

                }
                catch (ValidationException ex)
                {
                    errors.Add($"Line {index}. {ex.Message}");
                }

            }

            //make sure that the assigned managers are real employees
            foreach (var mgr in assignedManagers)
            {
                var companyId = mgr.Key.CompanyId;
                var managerId = mgr.Key.EmployeeId;

                if (!allEmployees.TryGetValue(new CompanyEmployeeId(companyId, managerId), out var employee))
                {
                    errors.Add($"Line {mgr.Value.LineNumber}. Manager {managerId} was not found as an employee in Company Id {companyId}");
                }
            }

            //now we can do the saving
            await TruncateData();

            var list=  _db.Companies.ToList();
            var list2=  _db.Employees
                    .Include(x=> x.Company)
                    .Include(x=> x.Manager)
                    .ToList();

            return errors;
        }

        private async Task TruncateData()
        {
                var cn = _db.Database.GetDbConnection();
                await cn.OpenAsync();
                using var cmd = cn.CreateCommand();
                cmd.CommandText = "exec TruncateData";
                await cmd.ExecuteNonQueryAsync();
        }
    }
}
