using PayrollSystem.DTO;
using System.Linq;

namespace PayrollSystem.RulesEngine
{
    public class PayCheckRules: IPayCheckRules
    {
        private readonly int paycheckBeforeDeduction;

        //Employee Benifits per year
        private readonly int employeeBenefitPerYear = 1000;

        // Dependent Benifit per year
        private readonly int dependentBenefitPerYear = 500;
 
        private decimal employeeBenefitPerPayCheck;
        private decimal employeeDepedentBenefitPerPayCheck;
        public PayCheckRules()
        {
            paycheckBeforeDeduction = 2000;
            SetEmployeeBenifits();
        }

        /// <summary>
        ///  Calculate FinalPaycheck after deductions
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        public decimal CalculatePayCheck(EmployeeDto employeeDto)
        {
           return ApplyRules(employeeDto);
        }

        /// <summary>
        ///  Set Employee & Dependents deductions
        /// </summary>
        private void SetEmployeeBenifits()
        {
            this.employeeBenefitPerPayCheck = employeeBenefitPerYear / 26;
            this.employeeDepedentBenefitPerPayCheck = dependentBenefitPerYear / 26;
        }

        /// <summary>
        /// Apply Benifits deductions
        /// </summary>
        /// <param name="employeeDto"></param>
        /// <returns></returns>
        private decimal ApplyRules(EmployeeDto employeeDto)
        {
            // Employee Benefit deductions
            var paycheckafterDeduction = CalculateEmployeeDeductible(employeeDto);

            paycheckafterDeduction = paycheckafterDeduction - CalculateDepedentDeductible(employeeDto);
            return paycheckafterDeduction;

        }

        private decimal CalculateEmployeeDeductible(EmployeeDto employeeDto)
        {
            decimal paycheckafterDeduction = 0;
            if (employeeDto.FirstName.ToLower().StartsWith("a"))
            {
                paycheckafterDeduction = paycheckBeforeDeduction - this.employeeBenefitPerPayCheck * (110 / 100);
            }
            else
            {
                paycheckafterDeduction = paycheckBeforeDeduction - this.employeeBenefitPerPayCheck;
            }
            return paycheckafterDeduction;
        }

        private decimal CalculateDepedentDeductible(EmployeeDto employeeDto)
        {
            decimal paycheckafterDeduction = 0;
            if (employeeDto.HasDependents)
            {
                if (employeeDto.Dependents.Any(s => s.FirstName.ToLower().StartsWith("a")))
                {
                    paycheckafterDeduction = paycheckafterDeduction - (this.employeeDepedentBenefitPerPayCheck * employeeDto.Dependents.Count);
                }
                else
                {
                    paycheckafterDeduction = paycheckafterDeduction - (this.employeeDepedentBenefitPerPayCheck * employeeDto.Dependents.Count);
                }
            }
            return paycheckafterDeduction;
        }
    }
}
