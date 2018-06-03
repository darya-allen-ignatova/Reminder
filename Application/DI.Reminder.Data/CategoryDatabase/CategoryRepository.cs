using DI.Reminder.Common.CategoryModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.Reminder.Data.CategoryDataBase
{
    public class CategoryRepository:ICategoryRepository
    {
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        public int? GetCategoryID(string Name)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                int? ID = null;
                sqlExpression = $"GetCategoryID";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@Name",
                    Value = Name
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ID = int.Parse(reader["ID"].ToString());
                }
                connection.Close();
                return ID;
            }
        }
        public int? GetCategoryParentID(string Name)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                int? parent = null;
                sqlExpression = $"GetCategoryID";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@Name",
                    Value = Name
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object ParentID = reader["ParentID"];
                    if (!Convert.IsDBNull(ParentID))
                    {
                        parent = int.Parse(ParentID.ToString());
                    }
                }
                connection.Close();
                return parent;
            }
        }
        public IList<Category> GetCategories(int? id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                if (id == null || id == 0)
                    sqlExpression = "GetCategories";
                else
                {
                    sqlExpression = "GetUnderCategories";
                }
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                if (sqlExpression == "GetUnderCategories")
                {
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = id
                    };
                    command.Parameters.Add(sqlparam);
                }
                SqlDataReader reader = command.ExecuteReader();
                List<Category> _list = new List<Category>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int? parent = null;
                        object ParentID = reader.GetValue(2);
                        if (!Convert.IsDBNull(ParentID))
                        {
                            parent = int.Parse(ParentID.ToString());
                        }
                        _list.Add(new Category() { ID = int.Parse(reader.GetValue(0).ToString()), Name = reader.GetValue(1).ToString(), ParentID = parent });
                    }
                    connection.Close();
                }
                return _list;
            }
        }
    }
}
