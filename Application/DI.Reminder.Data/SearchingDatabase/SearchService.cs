using DI.Reminder.Common.CategoryModel;
﻿using DI.Reminder.Common.Logger;
using DI.Reminder.Common.PromptModel;
using DI.Reminder.Data.CategoryDataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DI.Reminder.Data.SearchingDatabase
{
    public class SearchService : ISearchService
    {
        private ICategoryRepository _categoryRepository;
        private ILogger _logger;
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        public SearchService(ICategoryRepository categoryRepository, ILogger logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IList<Prompt> GetSearchResult(string promptval, string categoryval, string dateval, int UserID)
        {
            return GetPromptsAfterSearch(promptval, categoryval, dateval, UserID);
        }
        private IList<Prompt> GetSearchItems(int userID, int id, string value)
        {
            IList<Prompt> SearchingItems = null;
            switch (id)
            {
                case 1: SearchingItems = GetByCategory(value, userID); break;
                case 2: SearchingItems = GetByPrompt(value, userID); break;
                case 3: SearchingItems = GetByDate(value, userID); break;
            }
            return SearchingItems;
        }

        private IList<Prompt> GetByCategory(string value, int userID)
        {
            List<Prompt> _list = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "SearchByCategory";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam = new SqlParameter()
                    {
                        ParameterName = "@id",
                        Value = int.Parse(value)
                    };
                    command.Parameters.Add(sqlparam);
                    SqlParameter sqlparam1 = new SqlParameter()
                    {
                        ParameterName = "@userId",
                        Value = userID
                    };
                    command.Parameters.Add(sqlparam1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        _list = new List<Prompt>();
                        Category category = null;
                        while (reader.Read())
                        {
                            int IDCategory = int.Parse(reader["CategoryID"].ToString());
                            category = _categoryRepository.GetCategory(IDCategory);
                            int Id = int.Parse(reader["ID"].ToString());
                            _list.Add(new Prompt()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Category = category,
                                Date = Convert.ToDateTime(reader["DateOfCreating"].ToString()),
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
        private IList<Prompt> GetByPrompt(string value, int userID)
        {
            List<Prompt> _list = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    connection.Open();
                    string sqlExpression = "SearchByPrompt";
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        _list = new List<Prompt>();
                        Category category = null;
                        while (reader.Read())
                        {
                            int IDCategory = int.Parse(reader["CategoryID"].ToString());
                            category = _categoryRepository.GetCategory(IDCategory);
                            int Id = int.Parse(reader["ID"].ToString());
                            _list.Add(new Prompt()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Category = category,
                                Date = Convert.ToDateTime(reader["DateOfCreating"].ToString()),
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
        private IList<Prompt> GetByDate(string value, int userID)
        {
            List<Prompt> _list = null;
            DateTime dateTime;
            try
            {
                dateTime = Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
            try
            {
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
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        _list = new List<Prompt>();
                        Category category = null;
                        while (reader.Read())
                        {
                            int IDCategory = int.Parse(reader["CategoryID"].ToString());
                            category = _categoryRepository.GetCategory(IDCategory);
                            int Id = int.Parse(reader["ID"].ToString());
                            _list.Add(new Prompt()
                            {
                                ID = int.Parse(reader["ID"].ToString()),
                                Name = reader["Name"].ToString(),
                                Category = category,
                                Date = Convert.ToDateTime(reader["DateOfCreating"].ToString()),
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
        private IList<Prompt> GetPromptsAfterSearch(string promptval, string categoryval, string dateval, int UserID)
        {
            IList<Prompt> promptList = new List<Prompt>();
            IList<Prompt> result = new List<Prompt>();
            IList<Prompt> listOfPromptName = null;
            IList<Prompt> listOfCategoryName = null;
            IList<Prompt> listOfDate = null;
            if (promptval != null && promptval != "")
                listOfPromptName = GetSearchItems(UserID, 2, promptval);
            if (categoryval != null && categoryval != "0")
                listOfCategoryName = GetSearchItems(UserID, 1, categoryval);
            if (dateval != null && dateval != "")
                listOfDate = GetSearchItems(UserID, 3, dateval);
            if (listOfPromptName != null)
            {
                if (listOfCategoryName != null)
                {
                    foreach (var item in listOfPromptName)
                    {
                        if (listOfCategoryName.FirstOrDefault(g => g.ID == item.ID) != null)
                            result.Add(listOfCategoryName.First(g => g.ID == item.ID));
                    }
                    if (listOfDate != null)
                    {
                        foreach (var item in listOfDate)
                        {
                            if (result.FirstOrDefault(g => g.ID == item.ID) != null)
                                promptList.Add(result.First(g => g.ID == item.ID));
                        }
                        return promptList;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    if (listOfDate != null)
                    {
                        foreach (var item in listOfDate)
                        {
                            if (listOfPromptName.FirstOrDefault(g => g.ID == item.ID) != null)
                                result.Add(listOfPromptName.First(g => g.ID == item.ID));
                        }
                        return result;
                    }
                    else
                    {
                        return listOfPromptName;
                    }
                }
            }
            else
            {
                if (listOfCategoryName != null)
                {
                    if (listOfDate != null)
                    {
                        foreach (var item in listOfCategoryName)
                        {
                            if (listOfDate.FirstOrDefault(g => g.ID == item.ID) != null)
                                result.Add(listOfDate.First(g => g.ID == item.ID));
                        }
                        return result;
                    }
                    else
                        return listOfCategoryName;
                }
                else
                    return listOfDate;
            }
        }
    }
}
