using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
        string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private List<AdvertItem> GetListOfAdvertisings()
        {
            List<AdvertItem> _list = new List<AdvertItem>();
            using (SqlConnection connection = new SqlConnection(GetConnection))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {

                }
                string sqlExpression;
                sqlExpression = $"GetAdvertisings";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
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
            return _list;
        }
        public IList<AdvertItem> GetItems()
        {
            List<AdvertItem> itemsfromdatabase = GetListOfAdvertisings();
            List<AdvertItem> list = new List<AdvertItem>();
            for (int i = 0; i < 3; i++)
            {
                AdvertItem _advert = itemsfromdatabase.FirstOrDefault(f => f.ID == RandomItem(i*DateTime.Now.Second, itemsfromdatabase));
                list.Add(new AdvertItem()
                {
                    ID = _advert.ID, Title = _advert.Title, Url = _advert.Url, Image = _advert.Image
                });
            }
            return list;
        }
        public int RandomItem(int t, List<AdvertItem> itemslist)
        {
            Random rnd = new Random(t);
            return rnd.Next(1, itemslist.Count);
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