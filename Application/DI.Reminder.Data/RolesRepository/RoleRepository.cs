using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DI.Reminder.Common.LoginModels;

namespace DI.Reminder.Data.RolesRepository
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
        public void DeleteRole(int id)
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
        public void UpdateRole()
        {

        }
        public List<Role> GetRoleList(int? id)
        {
            List<Role> _rolelist;
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
            return _rolelist;
        }

        public IList<Role> GetAllRoles()
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "GetAllRoles";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                List<Role> _list = new List<Role>();
                while (reader.Read())
                {
                    _list.Add(new Role()
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Name = reader["Name"].ToString()
                    });
                }
                connection.Close();
                return _list;
            }
        }
    }
}
