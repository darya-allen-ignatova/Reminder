using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DI.Reminder.Common.LoginModels;
using DI.Reminder.Data.RolesRepository;

namespace DI.Reminder.Data.AccountDatabase
{
    public class AccountRepository : IAccountRepository
    {
        
        private IRoleRepository _rolerepository;
        public AccountRepository(IRoleRepository rolerepository)
        {
            _rolerepository = rolerepository;
        }
        private string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        public void InsertAccount(Account account)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "AddUser";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam1 = new SqlParameter()
                {
                    ParameterName = "@login",
                    Value = account.Login
                };
                command.Parameters.Add(sqlparam1);
                SqlParameter sqlparam2 = new SqlParameter()
                {
                    ParameterName = "@password",
                    Value = account.Password
                };
                
                command.Parameters.Add(sqlparam2);
                SqlParameter sqlparam3 = new SqlParameter()
                {
                    ParameterName = "@email",
                    Value = account.Email
                };

                command.Parameters.Add(sqlparam3);
                var result = command.ExecuteNonQuery();
                AddRoleForUser(account);
            }
        }
        private void AddRoleForUser(Account newaccount)
        {
            Account account = GetAccount(newaccount.Login);
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                for (int i = 0; i < newaccount.Roles.Count; i++)
                {

                    string sqlExpression = "AddConnection";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter sqlparam1 = new SqlParameter()
                    {
                        ParameterName = "@role",
                        Value = newaccount.Roles[i].Name
                    };
                    command.Parameters.Add(sqlparam1);
                    SqlParameter sqlparam2 = new SqlParameter()
                    {
                        ParameterName = "@userid",
                        Value = account.ID
                    };
                    command.Parameters.Add(sqlparam2);
                    var result = command.ExecuteNonQuery();
                }
            }
        }
        
        public Account GetAccount(string login)
        {
            Account account = null;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"GetUserByLogin";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter sqlparam = new SqlParameter()
                {
                    ParameterName = "@login",
                    Value = login
                };
                command.Parameters.Add(sqlparam);
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    account = new Account()
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        Email = reader["Email"].ToString()
                    };

                }
                connection.Close();

            }
            return account;
        }
        public Account GetAccount(int id)
        {
            Account account = null;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"GetUserByID";
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
                    account = new Account()
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString()
                    };

                }
                connection.Close();
                if (account == null)
                    return null;
                account.Roles = _rolerepository.GetRoleList(id);
            }
            return account;
        }
        public void DeleteAccount(int id)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = "DeleteUser";
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
        public void UpdateAccount(Account account)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"UpdatingUser";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@login", account.Login);
                command.Parameters.AddWithValue("@password", account.Password);
                command.Parameters.AddWithValue("@email", account.Email);
                command.Parameters.AddWithValue("@id", account.ID);
                var result = command.ExecuteNonQuery();
                connection.Close();
            }
            if(account.Roles==null)
            account.Roles = _rolerepository.GetRoleList(account.ID);
            List<int> IDs = GetIDOfRoles(account.ID);
            for (int i = 0; i < IDs.Count; i++)
            {
                if (account.Roles[i].Name.Replace(" ", string.Empty)==string.Empty)
                    _rolerepository.DeleteUserRole(IDs[i], account.ID);
                else
                {
                    using (SqlConnection connection = new SqlConnection(GetConnection))
                    {
                        connection.Open();
                        string sqlExpression = $"UpdatingUserRole";
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@roleid", IDs[i]);
                        command.Parameters.AddWithValue("@name", account.Roles[i].Name);
                        var result = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            if (IDs.Count < account.Roles.Count)
            {
                for (int i = IDs.Count; i < account.Roles.Count; i++)
                {
                    if (account.Roles[i].Name.Replace(" ", string.Empty) == string.Empty)
                        continue;
                    AddRolesForUser(account.Roles[i].Name, account.ID);
                }
            }
        }
        private void AddRolesForUser(string roleName, int userid)
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"AddRolesForUser";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@name", roleName);
                var result = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private List<int> GetIDOfRoles(int id)
        {
            List<int> IDs = new List<int>();
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression = $"GetUserRoles";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IDs.Add(int.Parse(reader["ID"].ToString()));
                }
                connection.Close();
            }
            return IDs;
        }
    
        public List<Account> GetAccountList()
        {
            List<Account> _accountlist;
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                connection.Open();
                string sqlExpression="GetAllUsers";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                _accountlist = new List<Account>();
                while (reader.Read())
                {
                    int id = int.Parse(reader["ID"].ToString());
                    List<Role> _rolelist = _rolerepository.GetRoleList(id);
                    _accountlist.Add(new Account()
                    {
                        ID = id,
                        Login = reader["Login"].ToString(),
                        Password = reader["Password"].ToString(),
                        Roles = _rolelist
                   });
                }
                connection.Close();
               
            }
            return _accountlist;
        }

    }
}
