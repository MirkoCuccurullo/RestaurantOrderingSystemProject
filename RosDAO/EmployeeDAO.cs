using RosModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace RosDAL
{
    public class EmployeeDAO : BaseDAO
    {

        public List<SecretQuestion> GetAllSecretQuestions()
        {
            string query = "SELECT QuestionID, Question FROM [SecretQuestion] WHERE QuestionID>0 ORDER BY [QuestionID]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadQuestions(ExecuteSelectQuery(query, sqlParameters));
        }

        public Employee GetEmployeeByUsername(string username)
        {
            string query = $"SELECT EmplID, Name, Username, Salt, Digest, SecretAnswer, Role FROM [Employee] WHERE Username = @Username;";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Username", username)
            };
            return ReadTable(ExecuteSelectQuery(query, sqlParameters));
        }

        public Employee GetEmployeeByUsernameAndPassword(string username, string password)
        {
            string query = $"SELECT EmplID, Name, Username, Salt, Digest, SecretAnswer, Role FROM [Employee] WHERE Username = @Username AND Salt = @Salt;";
            SqlParameter[] sqlParameters =
            {
                new SqlParameter("@Username", username)
                , new SqlParameter("@Salt", password)
            };
            return ReadTable(ExecuteSelectQuery(query, sqlParameters));
        }

        public void UpdatePassword(Employee employee)
        {
            string query = $"UPDATE Employee SET Salt = @Salt, Digest = @Digest WHERE EmplID = @EmplID;";
            SqlParameter[] sqlParameters = {
            new SqlParameter("@EmplID", employee.EmplID),
            new SqlParameter("@Salt", employee.Salt),
            new SqlParameter("@Digest", employee.Digest)          
            };

            ExecuteEditQuery(query, sqlParameters);
        }

        private Employee ReadTable(DataTable dataTable)
        {          
            try
            {
                Employee employee = new Employee();
                foreach (DataRow dr in dataTable.Rows)
                {                    
                    employee.EmplID = (int)dr["EmplID"];
                    employee.Name = (string)dr["Name"].ToString();
                    employee.Username = (string)dr["Username"].ToString();
                    employee.Salt = (string)dr["Salt"].ToString();
                    employee.Digest = (string)dr["Digest"].ToString();
                    employee.SecretAnswer = (string)dr["SecretAnswer"].ToString();
                    employee.Roles = (Roles)dr["Role"];                  
                }
                return employee;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }           
        }

        private List<SecretQuestion> ReadQuestions(DataTable dataTable)
        {
            List<SecretQuestion> questions = new List<SecretQuestion>();
            try
            {              
                foreach (DataRow dr in dataTable.Rows)
                {
                    SecretQuestion question = new SecretQuestion();
                    question.QuestionID = (int)dr["QuestionID"];
                    question.Question = (string)dr["Question"].ToString();
                    questions.Add(question);
                }

                return questions;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(Employee employee)
        {
            try
            {
                string query = "INSERT INTO Employee VALUES(@Name, @Username, @Salt, @Digest, @SecretAnswer, @Role);";

                SqlParameter[] sqlParameters = {
                new SqlParameter("@Name", employee.Name),
                new SqlParameter("@Username", employee.Username),
                new SqlParameter("@Salt", employee.Salt),
                new SqlParameter("@Digest", employee.Digest),
                new SqlParameter("@SecretAnswer", employee.SecretAnswer),
                new SqlParameter("@Role", employee.Roles)};

                ExecuteEditQuery(query, sqlParameters);
            }
            catch (Exception e)
            {
                throw new Exception("*Failed to register user*" + e.Message);
            }
        }
    }
}
