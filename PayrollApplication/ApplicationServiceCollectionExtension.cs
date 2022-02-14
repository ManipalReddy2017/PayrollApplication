using Microsoft.Extensions.DependencyInjection;
using PayrollSystem.RulesEngine;
using PayrollSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem
{
    public static class ApplicationServiceCollectionExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPayCheckRules, PayCheckRules>();
        }
    }
}
