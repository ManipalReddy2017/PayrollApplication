import React, { Component } from 'react';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export class AddEmployee extends Component {
    static displayName = "Add New Employee";
    constructor(props) {
        super(props);
        this.state =
        {
            firstName: '',
            lastName: '',
            email: '',
            spouseFirstName: '',
            spouseLastName: '',
            childFirstName: '',
            childLastName: '',
            payCheckBeforeDeductible: 2000,
            payCheckAfterDeductible: 0,
            empBenifitPerYear: 1000,
            dependentBenifitPerYear: 500,
            hasDependents: true,
        };

        // This binding is necessary to make `this` work in the callback
        this.saveEmployee = this.saveEmployee.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.calculatePayCheck = this.calculatePayCheck.bind(this);
    }
    saveEmployee() {
        this.calculatePayCheck();
        this.saveEmployeesData();
    };

    onFirstNameChange = e => {
        this.setState({
            firstName: e.target.value
        });
    };

    onLastNameChange = e => {
        this.setState({
            lastName: e.target.value
        });
    };

    onEmailChange = e => {
        this.setState({
            email: e.target.value
        });
    };

    handleChange = e => {
        this.setState({
            hasDependents: e.target.checked
        });
    };

    calculatePayCheck = e => {
        var hasEmployeeGotDiscount = false;
        if (this.state.firstName.toLowerCase().startsWith("a"))
        {
            hasEmployeeGotDiscount = true;
            var payCheckAfterCalculations = parseFloat(this.state.payCheckBeforeDeductible - ((this.state.empBenifitPerYear / 26)*(110/100))).toFixed(2);
        }
        else
        {
            var payCheckAfterCalculations = parseFloat(this.state.payCheckBeforeDeductible - (this.state.empBenifitPerYear / 26)).toFixed(2);
        }
        if (this.state.hasDependents)
        {
            if (this.state.spouseFirstName )
            {
                if (this.state.spouseFirstName.toLowerCase().startsWith("a") && !hasEmployeeGotDiscount)
                {
                    payCheckAfterCalculations = parseFloat(payCheckAfterCalculations - (this.state.dependentBenifitPerYear / 26 * (110 / 100))).toFixed(2);
                }
                else
                {
                    payCheckAfterCalculations = parseFloat(payCheckAfterCalculations - (this.state.dependentBenifitPerYear / 26)).toFixed(2);
                }
            }
            if (this.state.childFirstName)
            {
                if (this.state.childFirstName.startsWith("a") && !hasEmployeeGotDiscount)
                {
                    payCheckAfterCalculations = parseFloat(payCheckAfterCalculations - (this.state.dependentBenifitPerYear / 26 * (110 / 100))).toFixed(2);
                }
                else
                {
                    payCheckAfterCalculations = parseFloat(payCheckAfterCalculations - (this.state.dependentBenifitPerYear / 26)).toFixed(2);
                }
            }
        }
        this.setState({
            payCheckAfterDeductible: payCheckAfterCalculations
        });
    };
   

    render() {
        return (
            <div>
                <div className="form-group">
                    <label htmlFor="formGroupExampleInput">FirstName</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.firstName}
                        onChange={this.onFirstNameChange}
                        id="firstname"
                    />
                    <label htmlFor="formGroupExampleInput">LastName</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.lastName}
                        onChange={this.onLastNameChange}
                        id="lastname"
                    />
                    <label htmlFor="formGroupExampleInput">Email</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.email}
                        onChange={this.onEmailChange}
                        id="email"
                    />
                    <input type="checkbox" checked={this.state.hasDependents} onClick={this.handleChange} />
                    <label htmlFor="formGroupExampleInput">Enroll Dependents</label>
                    <br/>
                    <label htmlFor="formGroupExampleInput">Spouse FirstName</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.spouseFirstName}
                        onChange={(e) => this.setState({ spouseFirstName: e.target.value })}
                        disabled={!this.state.hasDependents}
                        id="spouseFirstName"
                    />
                    <label htmlFor="formGroupExampleInput">Spouse LastName</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.spouseLastName}
                        onChange={(e) => this.setState({ spouseLastName: e.target.value })}
                        disabled={!this.state.hasDependents}
                        id="spouseLastName"
                    />
                    <br />
                    <label htmlFor="formGroupExampleInput">Child FirstName</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.childFirstName}
                        onChange={(e) => this.setState({ childFirstName: e.target.value })}
                        disabled={!this.state.hasDependents}
                        id="childFirstName"
                    />
                    <label htmlFor="formGroupExampleInput">Child LastName</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.childLastName}
                        onChange={(e) => this.setState({ childLastName: e.target.value })}
                        disabled={!this.state.hasDependents}
                        id="childLastName"
                    />
                    <label htmlFor="formGroupExampleInput">PayCheck Deductible Amount</label>
                    <input
                        type="text"
                        className="form-control"
                        value={this.state.payCheckAfterDeductible}
                        disabled={true}
                        id="childLastName"
                    />
                </div>
                <button className="btn btn-primary mx-2" onClick={this.calculatePayCheck}>Preview PayCheck</button>
                <button className="btn btn-primary" onClick={this.saveEmployee}>Save Employee</button>
            </div>
        );
    }


    async saveEmployeesData() {
        try {
            var dependents = [
                {
                    firstName: this.state.spouseFirstName,
                    lastName: this.state.spouseLastName,
                    relation: 1
                },
                {
                    firstName: this.state.childFirstName,
                    lastName: this.state.childLastName,
                    relation: 2
                }
            ];
            fetch("employees", {
                // Adding method type
                method: "POST",
                // Adding body or contents to send
                body: JSON.stringify({
                    firstName: this.state.firstName,
                    lastName: this.state.lastName,
                    email: this.state.email,
                    hasDependents: this.state.hasDependents,
                    dependents: dependents,
                    payCheckBeforeDeductions: this.state.payCheckBeforeDeductible,
                    payCheckAfterDeductions: this.state.payCheckAfterDeductible,
                }),
                // Adding headers to the request
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                }
            })
                .then((response) => {
                    // Calling toast method by passing string
                    //toast('Employee Saved successfully');
                    this.props.history.push('/employeeslist')
                })
        }
        catch (error) {

        }
    }

    async getEmployeeData() {
        try {
            const response = await fetch('employees/?employeeId=1');
            const data = await response.json();
            this.setState(
                {
                    firstName: data[0].firstName,
                    lastName: data[0].firstName,
                    email: data[0].firstName,
                    spouseFirstName: data[0].firstName,
                    spouseLastName: data[0].firstName,
                    childFirstName: data[0].firstName,
                    childLastName: data[0].firstName,
                    hasDependents: false,
                    loading: false
                });
        }
        catch (error) {
        }
    }
}
