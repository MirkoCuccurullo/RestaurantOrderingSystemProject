using RosModel;
using RosLogic;
using System;
using System.Windows.Forms;

////////////////////Jason Xie, 659045, GROUP 1, IT1D////////////////////////////////////////////////////////////////////////////////////////////////

namespace RosUI
{
    public partial class Login : Form
    {
        private EmployeeLogic employeeLogic;
        private Tool tool;

        public Login()
        {
            InitializeComponent();
            employeeLogic = new EmployeeLogic();
            tool = new Tool();
        }

        //Will login the user in
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //check to see if all fields are filled
                if (txtUsername.Text == "" && txtPinCode.Text == "")
                {
                    MessageBox.Show("*Please your username and password*");
                    return;
                }
                else
                {
                    if (txtUsername.Text == "")
                    {
                        MessageBox.Show("*Please fill your username*");
                        return;
                    }
                    if (txtPinCode.Text == "")
                    {
                        MessageBox.Show("*Please fill your password*");
                        return;
                    }
                }

                Employee employee = employeeLogic.GetEmployeeByUsername(txtUsername.Text);       

                //Checks the login password and opens the corresponding view
                if (CheckPassword(employee))
                {
                    this.Hide();
                    RosMain main = new RosMain(employee);
                    if (employee.Roles == Roles.Waiter)
                    {
                        new TableOverview(employee, main).Show();
                    }
                    else                    
                    main.Show();
                }
                else
                {
                    MessageBox.Show("*Incorrect username or password*");
                    txtPinCode.Text = "";
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
                tool.WriteError(exp, exp.Message);
            }         
        }

        //opens forgot password form
        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            new ForgotPassword().Show();
        }

        //Opens the registration form
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                new Registration().Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
                tool.WriteError(exp, exp.Message);
            }
        }

        //Encrypts the password from the login and checks it with the one in the database
        private bool CheckPassword(Employee employee)
        {
            if (employee.Salt == null || employee.Digest == null)
                return false;

            if (employeeLogic.GetEmployeeByUsernameAndPassword(employee.Username, tool.HashPassword(txtPinCode.Text, employee.Salt).Digest) == null)
                return false;
            else
                return true;
        }     
    }
}
