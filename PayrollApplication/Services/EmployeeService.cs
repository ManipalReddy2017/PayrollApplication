using DataAccess;
using DataAccess.Enum;
using DataAccess.Model;
using PayrollSystem.DTO;
using PayrollSystem.RulesEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.Services
{
    public class EmployeeService: IEmployeeService
    {
        public IEmployeeRepository employeeRepository;
        public IPayCheckRules payCheckRules;
        public EmployeeService(IEmployeeRepository employeeRepository, IPayCheckRules payCheckRules)
        {
            this.employeeRepository = employeeRepository;
            this.payCheckRules = payCheckRules;
        }

        public async Task<List<EmployeeDto>> GetEmployees(int employeeId)
        {
            var employees = await this.employeeRepository.GetEmployeeAsync(employeeId);

            try
            {
                var employeesDetails = employees.Employees.Select(s => new EmployeeDto()
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PayCheckBeforeDeductions = s.PayCheckBeforeDeductions,
                    PayCheckAfterDeductions = s.PayCheckAfterDeductions,
                    Dependents = employees.Dependents.Where(s1 => s1.EmployeeId == s.EmployeeId).ToList()
                }).ToList();

                return employeesDetails;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> SaveEmployee(EmployeeDto data)
        {
            try
            {
                var addEmployeeRequest = new AddEmployeeRequest()
                {
                    firstName = data.FirstName,
                    lastName = data.LastName,
                    email = data.Email,
                    PayCheckBeforeDeductions = data.PayCheckBeforeDeductions,
                    PayCheckAfterDeductions = data.PayCheckAfterDeductions,
                    spouseFirstName = data.Dependents.Where(s=> s.Relation == Relation.Spouse).Select(s=> s.FirstName).FirstOrDefault(),
                    spouseLastName = data.Dependents.Where(s => s.Relation == Relation.Spouse).Select(s => s.LastName).FirstOrDefault(),
                    childFirstName = data.Dependents.Where(s => s.Relation == Relation.Child).Select(s => s.FirstName).FirstOrDefault(),
                    childLastName = data.Dependents.Where(s => s.Relation == Relation.Child).Select(s => s.LastName).FirstOrDefault()
                };
                var employees = await this.employeeRepository.SaveEmployeeAsync(addEmployeeRequest);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
