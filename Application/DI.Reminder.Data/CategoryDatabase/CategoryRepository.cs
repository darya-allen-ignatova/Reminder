using DI.Reminder.Common.CategoryModel;
using DI.Reminder.Common.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DI.Reminder.Data.CategoryDataBase
{
    public class CategoryRepository : ICategoryRepository
    {
        private ILogger _logger;
        public CategoryRepository(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            _list = new List<Category>();
                        }
                        while (reader.Read())
                        {
                            int parent = -1;
                            object ParentID = reader.GetValue(2);
                            if (!Convert.IsDBNull(ParentID))
                            {
                                parent = int.Parse(ParentID.ToString());
                            }
                            Category Parent = null;
                            if (parent != -1)
                            {
                                Parent = GetCategory(parent);
                            }
                            _list.Add(new Category()
                            {
                                ID = int.Parse(reader.GetValue(0).ToString()),
                                Name = reader.GetValue(1).ToString(),
                                ParentCategory = Parent
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
                    if (category.ParentCategory.ID != 0)
                    {
                        SqlParameter sqlparam2 = new SqlParameter()
                        {
                            ParameterName = "@ParentID",
                            Value = category.ParentCategory.ID
                        };
                        command.Parameters.Add(sqlparam2);
                    }
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
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int parentID = -1;
                            object ParentID = reader.GetValue(2);
                            if (!Convert.IsDBNull(ParentID))
                            {
                                parentID = int.Parse(ParentID.ToString());
                            }
                            Category Parent = null;
                            if (parentID != -1)
                            {
                                Parent = GetCategory(parentID);
                            }
                            category = new Category()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                ParentCategory = Parent
                            };
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            _list = new List<Category>();
                        }
                        while (reader.Read())
                        {
                            int parent = -1;
                            object ParentID = reader.GetValue(2);
                            if (!Convert.IsDBNull(ParentID))
                            {
                                parent = int.Parse(ParentID.ToString());
                            }
                            Category Parent = null;
                            if (parent != -1)
                            {
                                Parent = GetCategory(parent);
                            }
                            _list.Add(new Category()
                            {
                                ID = int.Parse(reader.GetValue(0).ToString()),
                                Name = reader.GetValue(1).ToString(),
                                ParentCategory = Parent
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
            return _list;
        }
        public void EditCategory(Category category)
        {
            int? ParentID = int.Parse(category.ParentCategory.ID.ToString().Replace(" ", string.Empty));
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
            catch (SqlException sqlExc)
            {
                _logger.Error("SqlException: " + sqlExc.Source + "\t" + sqlExc.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("SqlException: " + ex + "\t" + ex.Message);
            }
        }
    }
}
