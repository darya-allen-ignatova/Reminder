using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using DI.Reminder.Service.DataContract;

namespace DI.Reminder.Service.Advertising
{
    public class AdvertisingData
    {
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IList<AdvertisingItem> GetListOfAdvertisings()
        {
            List<AdvertisingItem> _list = new List<AdvertisingItem>();
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
                    _list.Add(new AdvertisingItem() { ID = int.Parse(reader["ID"].ToString()), Title = reader["Title"].ToString(), Url = reader["Url"].ToString(), Image = GetImageByteArray(Image) });
                }
                connection.Close();
            }
            return _list;
        }
        public IList<AdvertisingItem> GetItems()
        {
            IList<AdvertisingItem> itemsfromdatabase = GetListOfAdvertisings();
            IList<AdvertisingItem> list = new List<AdvertisingItem>();
            for (int i = 0; i < 3; i++)
            {
                AdvertisingItem _advert = itemsfromdatabase.FirstOrDefault(f => f.ID == RandomItem(i*DateTime.Now.Second, itemsfromdatabase));
                list.Add(new AdvertisingItem()
                {
                    ID = _advert.ID, Title = _advert.Title, Url = _advert.Url, Image = _advert.Image
                });
            }
            return list;
        }
        private int RandomItem(int t, IList<AdvertisingItem> itemslist)
        {
            Random rnd = new Random(t);
            return rnd.Next(1, itemslist.Count);
        }
        private byte[] GetImageByteArray(string imagePath)
        {
            if (imagePath == null)
                return null;
            string currentPath = HostingEnvironment.ApplicationPhysicalPath;
            var byteArray = File.ReadAllBytes(imagePath);
            return byteArray;

        }
    }
}