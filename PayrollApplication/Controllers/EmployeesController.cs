using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayrollSystem.DTO;
using PayrollSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCoreAppWithReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {

        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeService employeeService;
        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> Get(int employeeId)
        {
            var employees =  await employeeService.GetEmployees(employeeId);
            return employees;
        }

        [HttpPost]
        public async Task<bool> Post(EmployeeDto employeeDto)
        {
            return await employeeService.SaveEmployee(employeeDto);
        }
    }
}
