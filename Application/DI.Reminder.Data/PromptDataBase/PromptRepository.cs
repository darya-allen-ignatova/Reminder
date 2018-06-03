using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.CategoryDataBase;

namespace DI.Reminder.Data.PromptDataBase
{
    public class PromptRepository : IPromptRepository
    {
        
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }

        public IList<Prompt> GetPromptsList(int? id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression;
                if (id == 0)
                    sqlExpression = $"GetAllPrompts";
                else
                    sqlExpression = $"GetPromptsByID";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = id
                };
                if (sqlExpression == "GetPromptsByID")
                    command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                List<Prompt> _list = new List<Prompt>();
                while (reader.Read())
                {
                    object objcategory = reader["Category"];
                    string category = null;
                    if (!Convert.IsDBNull(objcategory))
                    {
                        category = objcategory.ToString();
                    }
                    _list.Add(new Prompt() { ID = int.Parse(reader["ID"].ToString()), Name = reader["Name"].ToString(), Category = category });
                }
                connection.Close();
                return _list;
            }
        }
        public Prompt GetPrompt(int? id)
        {
            Prompt prompt = null;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"GetPrompt";
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
                    int Id = int.Parse(reader["ID"].ToString());
                    object objimage = reader["Image"]; string image;
                    if (Convert.IsDBNull(objimage))
                        image = null;
                    else
                        image = objimage.ToString();
                    prompt = new Prompt()
                    {
                        ID = Id,
                        Name = reader["Name"].ToString(),
                        Category = reader["Category"].ToString(),
                        CreatingDate = Convert.ToDateTime(reader["DateOfCreating"].ToString()),
                        TimeOfPrompt = TimeSpan.Parse(reader["TimeOfPrompt"].ToString()),
                        Description = reader["Description"].ToString(),
                        Image = image,
                        Actions=GetActions(Id)
                    };
                }
                connection.Close();
            }
            return prompt;
        }
        private List<Common.PromptModel.Action> GetActions(int id)
        {
            List<Common.PromptModel.Action> actions = new List<Common.PromptModel.Action>();
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"GetActions";
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
                    actions.Add(new Common.PromptModel.Action
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Name = reader["ActionName"].ToString(),
                    });
                }
                connection.Close();
            }
            return actions;

        }
        public void AddPrompt(Prompt prompt, ICategoryRepository categoryRepository)
        {
            int? CategoryID = categoryRepository.GetCategoryID(prompt.Category);
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"AddPrompt";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", prompt.Name);
                command.Parameters.AddWithValue("@categoryid", CategoryID);
                command.Parameters.AddWithValue("@dataofcreating", DateTime.Now.Date);
                command.Parameters.AddWithValue("@description", prompt.Description);
                command.Parameters.AddWithValue("@Image", prompt.Image);
                command.Parameters.AddWithValue("@timeofprompt", prompt.TimeOfPrompt);
                var result = command.ExecuteScalar();
                prompt.ID = int.Parse(result.ToString());
                connection.Close();
            }
           
            AddActions(prompt);
        }
        public void DeletePrompt(int? id)
        {
            ICategoryRepository categoryRepository = new CategoryRepository();
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"DeletePrompt";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                var result = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void AddActions(Prompt prompt)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"AddAction";
                for (int i = 0; i < prompt.Actions.Count; i++)
                {
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", prompt.Actions[i].Name);
                    command.Parameters.AddWithValue("@PromptID",prompt.ID);
                    var result = command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}

