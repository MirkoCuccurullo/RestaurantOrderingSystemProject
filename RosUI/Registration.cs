using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RosLogic;
using RosModel;

////////////////////Jason Xie, 659045, GROUP 1, IT1D////////////////////////////////////////////////////////////////////////////////////////////////

namespace RosUI
{
    public partial class Registration : Form
    {
        private EmployeeLogic employeeLogic;
        private Employee employee;
        private List<SecretQuestion> questions;
        private Tool tool;

        public Registration()
        {
            InitializeComponent();
            employeeLogic = new EmployeeLogic();
            employee = new Employee();
            tool = new Tool();     
            questions = employeeLogic.GetAllSecretQuestions();

            foreach (SecretQuestion question in questions)
            {
                cmbSecret.Items.Add($"{question.Question}");
            }
        }

        //Will register the user and store it into the database
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {                             
                //Make sure all the fields are filled
                if (txtUsername.Text == "" || txtName.Text == "" || txtPinCode.Text == "" || txtSecretAnswer.Text == "" || cmbSecret.SelectedItem == null)
                {
                    MessageBox.Show("make sure to fill in all the fields");
                    return;
                }

                employee = new Employee();
                employee.PinCode = txtPinCode.Text;

                //Make sure the password and the role is correct
                if (CheckPassword(employee))
                {                
                    employee.Username = txtUsername.Text;
                    employee.Name = txtName.Text;                    
                    employee.SecretAnswer = txtSecretAnswer.Text;
                    employee.Roles = CheckRole(txtLicenseKey.Text);

                    //stores the informaion about the user
                    employeeLogic.Add(employee);
                    MessageBox.Show("Your Registration is Successfull");

                    this.Hide();
                    Login login = new Login();
                    login.Show();
                }
                else
                {
                    return;
                }            
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
                tool.WriteError(exp, exp.Message);
            }
        }

        //Goes back to the Login Form
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Login login = new Login();
                login.Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error");
                tool.WriteError(exp, exp.Message);
            }
        }

        //Check for a valid password and Encrypts the password and store it
        private bool CheckPassword(Employee employee)
        {
            //If the password is valid store the salt and digest
            if (IsValidPassword(employee.PinCode))
            {
                HashWithSaltResult hashResultSha256 = tool.HashPassword(txtPinCode.Text);
                employee.Salt = hashResultSha256.Salt;
                employee.Digest = hashResultSha256.Digest;
                return true;
            }
            else
            {
                txtPinCode.Text = "";
                MessageBox.Show("Your password must be a 4 digit pincode");
                return false;
            }
        }

        //Check with role the user will get according to the license key
        private Roles CheckRole(string licenseKey)
        {
            switch (licenseKey)
            {
                case "0001":
                    return Roles.Manager;

                case "0002":                
                    return Roles.Waiter;

                case "0003":                
                    return Roles.Chef;

                case "0004":               
                    return Roles.Bartender;

                case "0000":
                    return (int)Roles.Superuser;
            }
            MessageBox.Show("Please check if you have the correct license key");
            txtLicenseKey.Text = "";
            return Roles.None;
        }

        //Check if the password the user entered meets the requirement
        private bool IsValidPassword(string pincode)
        {
            if (ContainsNumber(pincode) && pincode.Length == 4)
            {
                return true;
            }
            return false;
        }

        //Check if the password contains numbers
        private bool ContainsNumber(string pincode)
        {
            foreach (char c in pincode)
            {
                if (c >= '0' && c <= '9')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
