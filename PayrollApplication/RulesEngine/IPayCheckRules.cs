using PayrollSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.RulesEngine
{
    public interface IPayCheckRules
    {
        decimal CalculatePayCheck(EmployeeDto employeeDto);
    }
}
