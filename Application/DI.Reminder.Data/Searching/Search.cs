using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.CategoryDataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DI.Reminder.Data.Searching
{
    public class Search:ISearch
    {
        ICategoryRepository _categoryRepository;
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        public Search(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        public IList<Prompt> GetSearchItems(int userID,int id, string value)
        {
            IList<Prompt> SearchingItems = null;
            switch (id)
            {
                case 1:  SearchingItems = GetByCategory(value, userID);break;
                case 2: SearchingItems = GetByPrompt(value, userID); break;
                case 3: SearchingItems = GetByDate(value, userID); break;
            }
            return SearchingItems;
        }
        
        private IList<Prompt> GetByCategory(string value, int userID)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "SearchByCategory";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                int? ID = _categoryRepository.GetCategoryID(value);
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@id",
                    Value = ID
                };
                command.Parameters.Add(sqlparam);
                SqlParameter sqlparam1 = new SqlParameter()
                {
                    ParameterName = "@userId",
                    Value = userID
                };
                command.Parameters.Add(sqlparam1);
                SqlDataReader reader = command.ExecuteReader();
                List<Prompt> _list = new List<Prompt>();
                while (reader.Read())
                {
                    _list.Add(new Prompt() {
                        ID = int.Parse(reader["ID"].ToString()),
                        Name = reader["Name"].ToString() });
                }
                connection.Close();
                return _list;
            }
        }
        private IList<Prompt> GetByPrompt(string value,  int userID)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression= "SearchByPrompt";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@name",
                    Value = value
                };
                command.Parameters.Add(sqlparam);
                SqlParameter sqlparam1 = new SqlParameter()
                {
                    ParameterName = "@userId",
                    Value = userID
                };
                command.Parameters.Add(sqlparam1);
                SqlDataReader reader = command.ExecuteReader();
                List<Prompt> _list = new List<Prompt>();
                while (reader.Read())
                {
                    _list.Add(new Prompt() {
                        ID = int.Parse(reader["ID"].ToString()),
                        Name = reader["Name"].ToString()
                    });
                }
                connection.Close();
                return _list;
            }
        }
        private IList<Prompt> GetByDate(string value,  int userID)
        {
            DateTime dateTime;
            try
            {
                dateTime = Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "SearchByDate";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@date",
                    Value = dateTime
                };
                command.Parameters.Add(sqlparam);
                SqlParameter sqlparam1 = new SqlParameter()
                {
                    ParameterName = "@userId",
                    Value = userID
                };
                command.Parameters.Add(sqlparam1);
                SqlDataReader reader = command.ExecuteReader();
                List<Prompt> _list = new List<Prompt>();
                while (reader.Read())
                {
                    _list.Add(new Prompt() {
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
