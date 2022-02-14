using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class AddEmployeeRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public decimal PayCheckAfterDeductions { get; set; }
        public decimal PayCheckBeforeDeductions { get; set; }
        public string spouseFirstName { get; set; }
        public string spouseLastName { get; set; }
        public string childFirstName { get; set; }
        public string childLastName { get; set; }
    }
}
