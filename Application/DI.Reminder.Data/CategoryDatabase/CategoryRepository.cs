using DI.Reminder.Common.CategoryModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DI.Reminder.Data.CategoryDataBase
{
    public class CategoryRepository : ICategoryRepository
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
                List<Category> _list = null;
                if (reader.HasRows)
                {
                    _list = new List<Category>();
                }
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
                return _list;
            }
        }

        public void AddCategory(Category category)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "AddCategory";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam1 = new SqlParameter()
                {
                    ParameterName = "@Name",
                    Value = category.Name
                };
                command.Parameters.Add(sqlparam1);
                SqlParameter sqlparam2 = new SqlParameter()
                {
                    ParameterName = "@ParentID",
                    Value = category.ParentID
                };
                command.Parameters.Add(sqlparam2);
                var result = command.ExecuteNonQuery();
            }
        }

        public void DeleteCategory(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "DeleteCategory";
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
        public Category GetCategory(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                Category category = null;
                string sqlExpression= $"GetCategory";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int? parent = null;
                    object ParentID = reader.GetValue(2);
                    if (!Convert.IsDBNull(ParentID))
                    {
                        parent = int.Parse(ParentID.ToString());
                    }
                    category = new Category()
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Name = reader["Name"].ToString(),
                        ParentID = parent
                    };
                }
                connection.Close();
                return category;
            }
        }
        public IList<Category> GetAllCategories()
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                sqlExpression = "GetAllCategories";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                List<Category> _list=null;
                if (reader.HasRows)
                {
                    _list = new List<Category>();
                }
                while (reader.Read())
                    {
                        int? parent = null;
                        object ParentID = reader.GetValue(2);
                        if (!Convert.IsDBNull(ParentID))
                        {
                            parent = int.Parse(ParentID.ToString());
                        }
                        _list.Add(new Category()
                        {
                            ID = int.Parse(reader.GetValue(0).ToString()),
                            Name = reader.GetValue(1).ToString(),
                            ParentID = parent
                        });
                    }
                connection.Close();
               
                return _list;
            }
        }
    }
}
