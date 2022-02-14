using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IEmployeeRepository
    {
        Task<EmployeesResponse> GetEmployeeAsync(int employeeId);
        Task<bool> SaveEmployeeAsync(AddEmployeeRequest addEmployee);
    }
}
