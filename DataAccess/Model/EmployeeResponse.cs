using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class EmployeesResponse
    {
        public List<Employee>  Employees { get; set; }
        public List<Dependent> Dependents { get; set; }
    }
}
