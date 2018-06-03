﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.RolesRepository;

namespace DI.Reminder.Data.AccountDatabase
{
    public class AccountRepository : IAccountRepository
    {
        private IRoleRepository _rolerepository;
        public AccountRepository(IRoleRepository rolerepository)
        {
            _rolerepository = rolerepository;
        }
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
                AddRoleForUser(account);
            }
        }
        private void AddRoleForUser(Account newaccount)
        {
            Account account = GetAccount(newaccount.Login);
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                for (int i = 0; i < newaccount.Roles.Count; i++)
                {

                    string sqlExpression = "AddConnection";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam1 = new SqlParameter()
                    {
                        ParameterName = "@role",
                        Value = newaccount.Roles[i].Name
                    };
                    command.Parameters.Add(sqlparam1);
                    SqlParameter sqlparam2 = new SqlParameter()
                    {
                        ParameterName = "@userid",
                        Value = account.ID
                    };
                    command.Parameters.Add(sqlparam2);
                    var result = command.ExecuteNonQuery();
                }
            }
        }
        
        public Account GetAccount(string login)
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
            List<Account> _accountlist;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression="GetAllUsers";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                _accountlist = new List<Account>();
                while (reader.Read())
                {
                    int id = int.Parse(reader["ID"].ToString());
                    List<Role> _rolelist = _rolerepository.GetRoleList(id);
                    _accountlist.Add(new Account()
                    {
                        ID = id,
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        Roles = _rolelist
                   });
                }
                connection.Close();
               
            }
            return _accountlist;
        }

    }
}
