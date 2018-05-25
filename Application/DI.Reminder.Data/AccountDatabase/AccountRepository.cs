using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Common.LoginModels;

namespace DI.Reminder.Data.AccountDatabase
{
    class AccountRepository : IAccountRepository
    {
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        public void InsertAccount(Account account)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "AddUser";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam1 = new SqlParameter()
                {
                    ParameterName = "@login",
                    Value = account.Login
                };
                command.Parameters.Add(sqlparam1);
                SqlParameter sqlparam2 = new SqlParameter()
                {
                    ParameterName = "@password",
                    Value = account.Password
                };
                command.Parameters.Add(sqlparam2);
                var result = command.ExecuteNonQuery();

                for (int i = 0; i < account.roles.Count; i++)
                {
                    sqlExpression = "AddConnection";
                    command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlparam1 = new SqlParameter()
                    {
                        ParameterName = "@role",
                        Value = account.roles[i].Name
                    };
                    command.Parameters.Add(sqlparam1);
                    sqlparam2 = new SqlParameter()
                    {
                        ParameterName = "@userid",
                        Value = account.ID
                    };
                    command.Parameters.Add(sqlparam2);
                    result = command.ExecuteNonQuery();
                }
            }
        }
        public Account GetAccount(string login, string password)
        {
            Account account = null;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"GetUser";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@login",
                    Value = login
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    account = new Account()
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString()
                    };

                }
                connection.Close();

            }
            return account;
        }
        public void DeleteAccount(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "DeleteUser";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam1 = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(sqlparam1);
                var result = command.ExecuteNonQuery();

            }
        }
        public void UpdateAccount()
        {

        }
        public List<Account> GetAccountList()
        {
            return new List<Account>();
        }

    }
}
