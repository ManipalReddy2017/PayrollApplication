using Dapper;
using DataAccess.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly string ConnectionString = null;

        private readonly IConfiguration configuration;
        public EmployeeRepository(IConfiguration config)
        {
            configuration = config;
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<EmployeesResponse> GetEmployeeAsync(int employeeId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var employeeRequest = new EmployeeRequest()
                {
                    EmployeeId = employeeId
                };
                var reader = connection.QueryMultiple("sp_SelectPayrollEmployee", employeeRequest, commandType: CommandType.StoredProcedure);
                var employeeDetails = reader.Read<Employee>()?.ToList();
                var dependents = reader.Read<Dependent>()?.ToList();
                var employeeResponse = new EmployeesResponse()
                {
                    Employees = employeeDetails,
                    Dependents = dependents
                };
                return employeeResponse;
            }
        }

        public async Task<bool> SaveEmployeeAsync(AddEmployeeRequest addEmployee)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    var employees = await connection.ExecuteAsync("sp_InsertEmployee", addEmployee, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
