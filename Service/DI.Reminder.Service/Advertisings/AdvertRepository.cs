using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace DI.Reminder.Service.Advertising
{
    public class AdvertRepository
    {
        public string GetConnection
        {
            get { return connection; }
            set { }
        }
        string connection = @"Data Source=LAPTOP-868QL38T\SQLEXPRESS;Initial Catalog=DI.Reminder.DataBase;Integrated Security=True";

        List<AdvertItem> _list = new List<AdvertItem>();
        public AdvertRepository()
        {
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                try
                { connection.Open(); }
                catch(SqlException)
                {

                }
                string sqlExpression;
                sqlExpression = $"SELECT *  FROM Advertising";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object objImage = reader["Image"];
                        string Image = null;
                        if (!Convert.IsDBNull(objImage))
                        {
                            Image = objImage.ToString();
                        }
                        _list.Add(new AdvertItem() { ID = int.Parse(reader["ID"].ToString()), Title = reader["Title"].ToString(), Url = reader["Url"].ToString(), Image = GetImageByteArray(Image) });
                    }
                    connection.Close();
                }
            }
        }
                
        
        public IEnumerable<AdvertItem> Items
        {
            get { return _list; }
        }
        public IList<AdvertItem> GetItems()
        {
            List<AdvertItem> list = new List<AdvertItem>();
            for (int i = 0; i < 3; i++)
            {
                AdvertItem _advert = _list.FirstOrDefault(f => f.ID == RandomItem(i*int.Parse(DateTime.Now.Second.ToString())));
                list.Add(new AdvertItem()
                {
                    ID = _advert.ID, Title = _advert.Title, Url = _advert.Url, Image = _advert.Image
                });
            }
            return list;
        }
        public int RandomItem(int t)
        {
            Random rnd = new Random(t);
            return rnd.Next(1, _list.Count);
        }
        public byte[] GetImageByteArray(string imagePath)
        {
            if (imagePath == null)
                return null;
            string currentPath = HostingEnvironment.ApplicationPhysicalPath;
            var byteArray = File.ReadAllBytes(imagePath);
            return byteArray;

        }
    }
}