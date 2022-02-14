using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.DTO
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PhoneNumber { get; set; }
        public string Gender { get; set; }
        public bool HasDependents { get; set; }
        public decimal PayCheckAfterDeductions { get; set; }
        public decimal PayCheckBeforeDeductions { get; set; }
        public List<Dependent> Dependents { get; set; }
    }
}
