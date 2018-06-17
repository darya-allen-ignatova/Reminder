﻿using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DI.Reminder.Common.LoginModels;

namespace DI.Reminder.Data.RoleDatabase
{
    public class RoleRepository : IRoleRepository
    {
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        public void InsertRole(string Role)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "AddRole";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@name",
                        Value = Role
                    };
                    command.Parameters.Add(sqlparam);
                    var result = command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }
        public void DeleteRole(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "DeleteRole";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = id
                    };
                    command.Parameters.Add(sqlparam);
                    var result = command.ExecuteNonQuery();

                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }
        public List<Role> GetRoleList(int? id)
        {
            List<Role> _rolelist = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression;
                    if (id == null)
                        sqlExpression = "GetAllRoles";
                    else
                        sqlExpression = "GetUserRoles";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    if (id != null)
                    {
                        SqlParameter sqlparam = new SqlParameter()
                        {
                            ParameterName = "@id",
                            Value = id
                        };
                        command.Parameters.Add(sqlparam);
                    }
                    SqlDataReader reader = command.ExecuteReader();
                    _rolelist = new List<Role>();
                    while (reader.Read())
                    {
                        _rolelist.Add(new Role
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            Name = reader["Name"].ToString()
                        });
                    }
                    connection.Close();

                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
            return _rolelist;
        }

        public IList<Role> GetAllRoles()
        {
            List<Role> _list = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "GetAllRoles";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    _list = new List<Role>();
                    while (reader.Read())
                    {
                        _list.Add(new Role()
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            Name = reader["Name"].ToString()
                        });
                    }
                    connection.Close();

                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
            return _list;
        }
        public void DeleteUserRole(int roleID, int userID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "DeleteUserRole";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@userID",
                        Value = userID
                    };
                    command.Parameters.Add(sqlparam);
                    SqlParameter sqlparam1 = new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = roleID
                    };
                    command.Parameters.Add(sqlparam1);
                    var result = command.ExecuteNonQuery();

                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }
    }
}