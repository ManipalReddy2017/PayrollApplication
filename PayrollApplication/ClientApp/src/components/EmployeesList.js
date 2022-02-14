import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
export class EmployeesList extends Component {
  static displayName = "Employees List";

  constructor(props) {
    super(props);
      this.state = { employees: [], loading: true };
      this.getDependentName = this.getDependentName.bind(this);
  }

    componentDidMount()
    {
        this.populateEmployeesData();
    }

    getDependentName(dependents,dependenttype)
    {
        var dependentResult  = dependents.filter(function (dependent) {
            return dependent.habitat == dependenttype;
        });
        return dependentResult.firstName + dependentResult.lastName;
    };
 

    static renderForecastsTable(employees) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Employee</th>
            <th>Spouse</th>
            <th>Child</th>
            <th>PayCheckBeforeeductions</th>
            <th>PayCheckAfterDeductions</th>
            <th>Email</th>
          </tr>
        </thead>
        <tbody>
                {employees.map(employee =>
                    <tr key={employee.employeeId}>
                        <td><a href=''>{employee.firstName} {employee.lastName}</a></td>
                        <td>{employee.dependents[0]?.firstName} {employee.dependents[0]?.lastName}</td>
                        <td>{employee.dependents[1]?.firstName} {employee.dependents[1]?.lastName}</td>
                        <td>{employee.payCheckBeforeDeductions}</td>
                        <td>{employee.payCheckAfterDeductions}</td>
                        <td>{employee.email}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : EmployeesList.renderForecastsTable(this.state.employees);

    return (
      <div>
        <h1 id="tabelLabel" >Employees Details</h1>
        {contents}
      </div>
    );
  }

  async populateEmployeesData() {
      const response = await fetch('employees');
      debugger;
     const data = await response.json();
    debugger;
    this.setState({ employees: data, loading: false });
  }
}
