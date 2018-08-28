using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DI.Reminder.Common.Logger;
using DI.Reminder.Common.LoginModels;

namespace DI.Reminder.Data.RoleDatabase
{
    public class RoleRepository : IRoleRepository
    {
        private ILogger _logger;
        public RoleRepository(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
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
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }
        public bool DeleteRole(int id)
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
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
                return false;
            }
            return true;
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
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
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
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
            return _list;
        }
       

        public Role GetRoleByName(string Name)
        {
            Role role = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "GetRole";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@roleName",
                        Value = Name
                    };
                    command.Parameters.Add(sqlparam);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        role = new Role()
                        {
                            ID=int.Parse(reader["ID"].ToString()),
                            Name=reader["Name"].ToString()
                        };
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
            return role;
        }
    }
}
