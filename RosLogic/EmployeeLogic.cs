using System.Collections.Generic;
using RosModel;
using RosDAL;

namespace RosLogic
{
    public class EmployeeLogic
    {
        EmployeeDAO employeedb = new EmployeeDAO();


        public void UpdatePassword(Employee employee)
        {
            employeedb.UpdatePassword(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            return employeedb.GetAllEmployees();
        }

        public List<SecretQuestion> GetAllSecretQuestions()
        {
            return employeedb.GetAllSecretQuestions();
        }

        public Employee GetEmployeeByUsername(string username)
        {
            return employeedb.GetEmployeeByUsername(username);
        }

        public Employee GetEmployeeByUsernameAndPassword(string username, string password)
        {
            return employeedb.GetEmployeeByUsernameAndPassword(username, password);
        }

        public void Add(Employee employee)
        {
            employeedb.Add(employee);
        }
    }
}
