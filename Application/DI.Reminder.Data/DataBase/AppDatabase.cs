using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DI.Reminder.Data.DataBase
{
    public class AppDatabase : IGetData
    {
        public string GetConnection
        {
            get { return connection; }
            set { }
        }
        string connection = @"Data Source=LAPTOP-868QL38T\SQLEXPRESS;Initial Catalog=DI.Reminder.DataBase;Integrated Security=True";

        public IList<DataPrompt> GetItems(int? id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                if (id == null)
                    return null;
                connection.Open();
                string sqlExpression;
                if (id == 0)
                    sqlExpression = $"SELECT p.ID, p.Name, cat.Name AS Category FROM Prompts p INNER JOIN Categories cat ON p.CategoryID = cat.ID ";
                else
                    sqlExpression = $"SELECT p.ID, p.Name, cat.Name AS Category FROM Prompts p INNER JOIN Categories cat ON p.CategoryID = cat.ID WHERE CategoryID = @id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                List<DataPrompt> _list = new List<DataPrompt>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object objcategory = reader["Category"];
                        string category = null;
                        if(!Convert.IsDBNull(objcategory))
                        {
                            category = objcategory.ToString();
                        }
                        _list.Add(new DataPrompt() { ID = int.Parse(reader["ID"].ToString()), Name = reader["Name"].ToString(), Category=category});
                    }
                    connection.Close();
                }
                return _list;
            }
        }
        public DataPrompt GetPrompt(int id)
        {
            DataPrompt prompt = new DataPrompt();
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression= $"SELECT p.ID, p.Name, cat.Name AS Category,p.DateOfCreating, p.TimeOfPrompt, p.Description, p.Image FROM Prompts p INNER JOIN Categories cat ON p.CategoryID = cat.ID WHERE p.ID = @id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       
                        object objimage = reader["Image"];string image;
                        if (Convert.IsDBNull(objimage))
                            image = null;
                        else
                            image = objimage.ToString();
                        prompt = new DataPrompt()
                        {
                            ID = int.Parse(reader["ID"].ToString()),
                            Name = reader["Name"].ToString(),
                            Category = reader["Category"].ToString(),
                            CreatingDate = Convert.ToDateTime(reader["DateOfCreating"].ToString()),
                            TimeOfPrompt = TimeSpan.Parse(reader["TimeOfPrompt"].ToString()),
                            Description = reader["Description"].ToString(),
                            Image = image
                    } ;
                    }
                    connection.Close();
                }
            }
            return prompt;
        }
        public int? GetCategoryID(string Name)
        {
            if (Name == null)
                return null;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                int? parent = null;
                sqlExpression = $"SELECT ParentID FROM Categories WHERE Name=@Name";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@Name",
                    Value = Name
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
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
                return parent;
            }
        }
        public IList<DataCategory> GetCategories(int? id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                if (id == null)
                    sqlExpression = $"SELECT * FROM Categories WHERE ParentID IS NULL ";
                else
                {
                    sqlExpression = $"SELECT * FROM Categories WHERE ParentID=@id";
                }
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                if(id != null)
                {
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = id
                    };
                    command.Parameters.Add(sqlparam);
                }
                SqlDataReader reader = command.ExecuteReader();
                List<DataCategory> _list = new List<DataCategory>();
             
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int? parent = null;
                        object ParentID = reader.GetValue(2);
                        if (!Convert.IsDBNull(ParentID))
                        {
                            parent=int.Parse(ParentID.ToString());
                        }
                        _list.Add(new DataCategory() { ID = int.Parse(reader.GetValue(0).ToString()), Name = reader.GetValue(1).ToString(), ParentID=parent });
                    }
                    connection.Close();
                }
                return _list;
            }
        }

    }
}
