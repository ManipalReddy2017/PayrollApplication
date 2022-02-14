using PayrollSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployees(int employeeId);
        Task<bool> SaveEmployee(EmployeeDto data);
    }
}
