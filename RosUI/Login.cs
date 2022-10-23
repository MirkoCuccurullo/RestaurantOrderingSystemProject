using RosModel;
using RosLogic;
using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;

////////////////////Jason Xie, 659045, GROUP 1, IT1D////////////////////////////////////////////////////////////////////////////////////////////////

namespace RosUI
{
    public partial class Login : Form
    {
        private RosMain main;
        private Employee employee;
        private EmployeeLogic employeeLogic;
        private List<Employee> employees;


        public Login()
        {
            InitializeComponent();
            employeeLogic = new EmployeeLogic();
            employee = new Employee();
            main = new RosMain(employee);
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

                employee = employeeLogic.GetEmployeeByUsername(txtUsername.Text);       

                //Checks the login password and opens the corresponding view
                if (CheckPassword())
                {
                    this.Hide();
                    main = new RosMain(employee);
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
                WriteError(exp, exp.Message);
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
                new Registration(main).Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
                WriteError(exp, exp.Message);
            }
        }

        //Encrypts the password from the login and checks it with the one in the database
        private bool CheckPassword()
        {
            if (employee.Salt == null || employee.Digest == null)
                return false;

            if (employeeLogic.GetEmployeeByUsernameAndPassword(employee.Username, HashPassword(txtPinCode.Text, employee.Salt).Digest) == null)
                return false;
            else
                return true;
        }

        //Write error to text file
        private void WriteError(Exception e, string errorMessage)
        {
            StreamWriter writer = File.AppendText("ErrorLog.txt");
            writer.WriteLine($"Error occurred: {errorMessage}");
            writer.WriteLine(e);
            writer.WriteLine(DateTime.Now);
            writer.Close();
        }

        //This method hashes the password
        private HashWithSaltResult HashPassword(string password, string salt)
        {
            PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            HashWithSaltResult hashResultSha256 = pwHasher.Hash(password, SHA256.Create(), salt);

            return hashResultSha256;
        }
    }
}
