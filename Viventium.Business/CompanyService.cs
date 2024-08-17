using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

using Viventium.DTOs;
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

        public async Task<DTOs.CompanyHeader[]> GetCompanies()
        {
            var data = await _db.Companies
                .Select(x => new DTOs.CompanyHeader()
                {
                    Code = x.Code,
                    Description = x.Description,
                    Id = x.CompanyId,
                    EmployeeCount = x.Employees!.Count()
                })
                .ToArrayAsync();
            return data;
        }

        public async Task<DTOs.Company?> GetCompany(int companyId)
        {
            var data = await _db.Companies
                .Include(x=> x.Employees)
                .Where(x=> x.CompanyId == companyId)
                .Select(x=>new DTOs.Company()
                {
                    Code = x.Code,
                    Description = x.Description,
                    Id = x.CompanyId,
                    EmployeeCount = x.Employees!.Count(),
                    Employees = x.Employees!.Select(x=> new DTOs.EmployeeHeader()
                    {
                        EmployeeNumber = x.EmployeeNumber,
                        FullName    = x.FirstName + " " + x.LastName
                    }).ToArray()
                })
                .FirstOrDefaultAsync();
            return data;
        }

        //this version of GetEmployee, I decied to retrive all employees and then loop recursivelly to find all the managers
        //previos version, required 2 queries , one to get the current employee and another one to retrieve all .Si I decied to use one query

        public async Task<Employee?> GetEmployee(int companyId, string employeeNumber)
        {

            //retriev all employees so I can
            var employeeNumberToEmployee = await _db.Employees
                .Where(x=> x.CompanyId == companyId)
                .Select(x=> new DTOs.Employee()
                {
                    Department=x.Department,
                    Email=x.Email,
                    EmployeeNumber = x.EmployeeNumber,
                    FullName=x.FirstName + " " + x.LastName,
                    HireDate=x.HireDate,
                    ManagerEmployeeNumber = x.ManagerEmployeeNumber,
                    Managers = new EmployeeHeader[] { } // List of EmployeeHeaders of the managers, ordered ascending by seniority (i.e. the immediate manager first)
                })
                .ToDictionaryAsync(x=> x.EmployeeNumber);


            if (employeeNumberToEmployee.Count() ==0)
                return null;

            if (!employeeNumberToEmployee.TryGetValue(employeeNumber, out var currentEmployee))
                return null;

            List<Employee> managers = new();
            AddManagers(employeeNumberToEmployee, managers, currentEmployee);
            var data = employeeNumberToEmployee[employeeNumber];
            data.Managers = managers.Select(x => new EmployeeHeader() { EmployeeNumber = x.EmployeeNumber, FullName = x.FullName }).ToArray();
            return data;
        }


        //public async Task<Employee?> GetEmployee(int companyId, string employeeNumber)
        //{
        //    var data = await _db.Employees
        //        .Where(x => x.CompanyId == companyId && x.EmployeeNumber == employeeNumber)
        //        .Select(x => new DTOs.Employee()
        //        {
        //            Department = x.Department,
        //            Email = x.Email,
        //            EmployeeNumber = employeeNumber,
        //            FullName = x.FirstName + " " + x.LastName,
        //            HireDate = x.HireDate,
        //            Managers = new EmployeeHeader[] { } // List of EmployeeHeaders of the managers, ordered ascending by seniority (i.e. the immediate manager first)
        //        })
        //        .FirstOrDefaultAsync();
        //    if (data is null)
        //        return data;

        //    var managers = await GetManagers(companyId, employeeNumber);
        //    data.Managers = managers;
        //    return data;
        //}

        //private async Task<EmployeeHeader[]> GetManagers(int companyId, string employeeNumber)
        //{
        //    //save all employees
        //    var employeeNumberToEmployee = await _db.Employees.Where(x => x.CompanyId == companyId)
        //            .Select(x=> new EmployeeNumberFullNameManager (x.EmployeeNumber, x.FirstName + " " + x.LastName, x.ManagerEmployeeNumber))
        //            .ToDictionaryAsync(x => x.EmployeeNumber);

        //    var currentEmployee = employeeNumberToEmployee[employeeNumber]; //this wont fail since at this point the employee is present. UNLESS it was deleted during the call
        //    List<EmployeeNumberFullNameManager> managers = new ();

        //    AddManagers(employeeNumberToEmployee, managers, currentEmployee);


        //    return   managers.Select(x=>new EmployeeHeader() {
        //        EmployeeNumber = x.EmployeeNumber,
        //        FullName = x.FullName
        //    }).ToArray();
        //}

        private void AddManagers(Dictionary<string, Employee> allEmployees, List<Employee> managers, Employee currentEmployee)
        {
            if (currentEmployee.ManagerEmployeeNumber is null)
                return;

            var managerEmployee = allEmployees[currentEmployee.ManagerEmployeeNumber];
            managers.Add(managerEmployee);

            AddManagers(allEmployees, managers, managerEmployee);
        }

        public async Task<List<string>> ImportCSV(Stream stream)
        {

            List<string> errors = new List<string>();

            using var sr = new StreamReader(stream);
            int index = 0;
            Dictionary<CompanyEmployeeId, ImportModel> assignedManagers = new();
            Dictionary<CompanyEmployeeId, ImportModel> allEmployees = new();

            Dictionary<int, Models.DB.Company> companies = new();


            //skip first line with headers
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


                    //Validation #1. The employeeNumber should be unique within a given company. 
                    if (allEmployees.ContainsKey(new CompanyEmployeeId(model.CompanyId, model.EmployeeNumber)))
                    {
                        throw new ValidationException($"{model.CompanyId} {model.EmployeeNumber} already exists  in {model.LineNumber}");
                    }

                    allEmployees.Add(new CompanyEmployeeId(model.CompanyId, model.EmployeeNumber), model);




                    //store the manager so I can check them for valid employee once all employees are parsed.
                    if (model.ManagerEmployeeNumber is not null)
                    {
                        assignedManagers[new(model.CompanyId, model.ManagerEmployeeNumber)] = model;
                    }

                }
                catch (ValidationException ex)
                {
                    errors.Add($"Line {index}. {ex.Message}");
                }

            }

            //make sure that the assigned managers are real employees
            foreach (var emp in allEmployees)
            {
                var eoi = emp.Value; //Employee to Import

                //Validation #2. The manager of the given employee should exist in the same company.
                if (eoi.ManagerEmployeeNumber is not null)
                {
                    //check if manager is an employee
                    if (!allEmployees.TryGetValue(new CompanyEmployeeId(eoi.CompanyId, eoi.ManagerEmployeeNumber), out var employee))
                    {
                        errors.Add($"Line {eoi.LineNumber}. Manager {eoi.ManagerEmployeeNumber} was not found as an employee in Company Id {eoi.CompanyId}");
                        continue;
                    }
                }


                //check if the company as been added already. if not create it and put in the db context to be saved later.
                if (!companies.TryGetValue(eoi.CompanyId, out var company))
                {
                    company = new Models.DB.Company()
                    {
                        Code = eoi.CompanyCode,
                        CompanyId = eoi.CompanyId,
                        Description = eoi.CompanyDescription
                    };
                    companies.Add(company.CompanyId, company);
                    _db.Add(company);
                }
                _db.Add(new Models.DB.Employee()
                {
                    CompanyId = company.CompanyId,
                    Department =eoi.EmployeeDepartment,
                    Email = eoi.EmployeeEmail,
                    EmployeeNumber = eoi.EmployeeNumber,
                    FirstName   =eoi.EmployeeFirstName,
                    LastName =eoi.EmployeeLastName,
                    HireDate=eoi.HireDate,
                    ManagerEmployeeNumber = eoi.ManagerEmployeeNumber
                });
                //if the employee is ready to be stored

            }


            if (errors.Count == 0)
            {
                //now we can do the saving. Abort and rollback if there is any error
                var transaction = await _db.Database.BeginTransactionAsync();
                await _db.Employees.ExecuteDeleteAsync();
                await _db.Companies.ExecuteDeleteAsync();
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return errors;
        }
 
    }
}
