using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

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

        public IList<DataPrompt> GetItems(int id)
        {
            //var NameOfConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                if (id == 0)
                    sqlExpression = $"SELECT * FROM Prompts ";
                else
                    sqlExpression = $"SELECT p.ID, p.Name FROM Prompts p INNER JOIN Categories cat ON p.CategoryID = cat.ID WHERE CategoryID = @id";
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
                        _list.Add(new DataPrompt() { ID = int.Parse(reader["ID"].ToString()), Name = reader["Name"].ToString() });
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
                        DateTime dt = DateTime.Parse(reader["DateOfCreating"].ToString());
                        //object image = reader["Image"];
                        //image = image != null ? (byte[])image : null;
                        prompt =new DataPrompt()
                        {   ID = int.Parse(reader["ID"].ToString()),
                            Name = reader["Name"].ToString(),
                            Category =reader["Category"].ToString(),
                            //CreatingDate =dt.ToShortDateString(),
                            TimeOfPrompt =TimeSpan.Parse(reader["TimeOfPrompt"].ToString()),
                            Description = reader["Description"].ToString(),
                            //Image = (byte[])(reader["Image"])
                    } ;
                    }
                    connection.Close();
                }
            }
            return prompt;
        }
        public List<DataCategories> GetCategories(int? id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                if (id == null)
                    sqlExpression = $"SELECT * FROM Categories WHERE ParentID IS NULL ";
                else
                    sqlExpression = $"SELECT * FROM Categories WHERE ParentID=@id";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                List<DataCategories> _list = new List<DataCategories>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        _list.Add(new DataCategories() { ID = int.Parse(reader["ID"].ToString()), Name = reader["Name"].ToString(), ParentID=int.Parse(reader["ParentID"].ToString()) });
                    }
                    connection.Close();
                }
                return _list;
            }
        }

    }
}
