using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PhoneNumber { get; set; }
        public string Gender { get; set; }
        public decimal PayCheckAfterDeductions { get; set; }
        public decimal PayCheckBeforeDeductions { get; set; }
        public List<Dependent> Dependents { get; set; }
    }
}
