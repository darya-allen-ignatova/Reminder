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
            int? ID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression;

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
            return ID;
        }
        public int? GetCategoryParentID(string Name)
        {
            int? parent = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression;

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
            return parent;
        }
        public IList<Category> GetCategories(int? id)
        {
            List<Category> _list = null;
            try
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

        public void AddCategory(Category category)
        {
            try
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
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }

        public void DeleteCategory(int id)
        {
            try
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
            catch (SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }
        public Category GetCategory(int id)
        {
            Category category = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();

                    string sqlExpression = $"GetCategory";
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
            return category;
        }
        public IList<Category> GetAllCategories()
        {
            List<Category> _list = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression;
                    sqlExpression = "GetAllCategories";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();

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
        public void EditCategory(Category category)
        {
            int? ParentID = GetCategoryParentID(category.Name);
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = null;
                    if (ParentID == null)
                        sqlExpression = $"UpdatingCategoryWithNull";
                    else
                        sqlExpression = $"UpdatingCategory";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", category.Name);
                    if (ParentID != null)
                        command.Parameters.AddWithValue("@parentID", ParentID);
                    command.Parameters.AddWithValue("@id", category.ID);
                    var result = command.ExecuteNonQuery();
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
        }
    }
}
