﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DI.Reminder.Common.Logger;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.RoleDatabase;

namespace DI.Reminder.Data.AccountDatabase
{
    public class AccountRepository : IAccountRepository
    {

        private IRoleRepository _rolerepository;
        private ILogger _logger;
        public AccountRepository(IRoleRepository rolerepository, ILogger logger)
        {
            _rolerepository = rolerepository ?? throw new ArgumentNullException(nameof(rolerepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        public void InsertAccount(Account account)
        {
            try
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
                    SqlParameter sqlparam3 = new SqlParameter()
                    {
                        ParameterName = "@email",
                        Value = account.Email
                    };

                    command.Parameters.Add(sqlparam3);
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    AddRoleForUser(account);
                }

            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }
        private void AddRoleForUser(Account newaccount)
        {
            try
            {
                Account account = GetAccount(newaccount.Login);

                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();string sqlExpression = "AddConnection";
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter sqlparam1 = new SqlParameter()
                        {
                            ParameterName = "@roleid",
                            Value = newaccount.Role.ID
                        };
                        command.Parameters.Add(sqlparam1);
                        SqlParameter sqlparam2 = new SqlParameter()
                        {
                            ParameterName = "@userid",
                            Value = account.ID
                        };
                        command.Parameters.Add(sqlparam2);
                        var result = command.ExecuteNonQuery();
                    
                    connection.Close();
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }

        public Account GetAccount(string login)
        {
            Account account = null;
            try
            {

                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "GetUserByLogin";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@login",
                        Value = login
                    };
                    command.Parameters.Add(sqlparam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Login = reader["Login"].ToString(),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString()
                            };

                        }
                    }
                    connection.Close();
                    if (account == null)
                        return null;
                    account.Role = _rolerepository.GetRole(account.ID);

                }

            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
            return account;
        }
        public Account GetAccount(int id)
        {

            Account account = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "GetUserByID";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = id
                    };
                    command.Parameters.Add(sqlparam);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new Account()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Login = reader["Login"].ToString(),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString()
                            };

                        }
                    }
                    connection.Close();
                    if (account == null)
                        return null;
                    account.Role = _rolerepository.GetRole(id);
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
            return account;
        }
        public void DeleteAccount(int id)
        {
            try
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
                    connection.Close();
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }
        public void AdminUpdateAccount(Account account)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "UpdatingUser";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@login", account.Login);
                    command.Parameters.AddWithValue("@password", account.Password);
                    command.Parameters.AddWithValue("@email", account.Email);
                    command.Parameters.AddWithValue("@id", account.ID);
                    var result = command.ExecuteNonQuery();

                    sqlExpression = "UpdatingUserRole";
                    command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@roleid", account.Role.ID);
                    command.Parameters.AddWithValue("@userid", account.ID);
                    result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }
        public void UpdateAccount(Account account)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "UpdatingUser";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@login", account.Login);
                    command.Parameters.AddWithValue("@password", account.Password);
                    command.Parameters.AddWithValue("@email", account.Email);
                    command.Parameters.AddWithValue("@id", account.ID);
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }
        private void AddRolesForUser(string roleName, int userid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "AddRolesForUser";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userid", userid);
                    command.Parameters.AddWithValue("@name", roleName);
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }
       

        public List<Account> GetAccountList()
        {
            List<Account> _accountlist = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "GetAllUsers";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        _accountlist = new List<Account>();
                        while (reader.Read())
                        {
                            int id = int.Parse(reader["ID"].ToString());
                            Role _role = _rolerepository.GetRole(id);
                            _accountlist.Add(new Account()
                            {
                                ID = id,
                                Login = reader["Login"].ToString(),
                                Password = reader["Password"].ToString(),
                                Role = _role
                            });
                        }
                    }
                    connection.Close();

                }
            }
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
            return _accountlist;
        }

    }
}
