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
        private int countOfItems = 3;
        private string GetConnection
        {
            get { return connection; }
            set { }
        }
        string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IList<AdvertisingItem> GetListOfAdvertisings()
        {
            List<AdvertisingItem> _list = new List<AdvertisingItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnection))
                {
                    try
                    {
                        connection.Open();
                    }
                    catch (SqlException)
                    {
                        throw;
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
            }
            catch(SqlException)
            {
                throw;
            }
            catch
            {
                throw;
            }
            return _list;
        }
        public IList<AdvertisingItem> GetItems()
        {
            IList<AdvertisingItem> itemsfromdatabase = GetListOfAdvertisings();
            IList<AdvertisingItem> list = new List<AdvertisingItem>();
            int[] random = RandomArray(itemsfromdatabase.Count);
            for (int i = 0; i < countOfItems; i++)
            {
                AdvertisingItem _advert = itemsfromdatabase.FirstOrDefault(f => f.ID == random[i]);
                list.Add(new AdvertisingItem()
                {
                    ID = _advert.ID, Title = _advert.Title, Url = _advert.Url, Image = _advert.Image
                });
            }
            return list;
        }
        private int[] RandomArray(int totalCount)
        {
            int[] array = new int[countOfItems];
            for (int i = 0; i < array.Length;)
            {
                bool flag = true;
                Random rnd = new Random(i *DateTime.Now.Millisecond+10);
                int number= rnd.Next(1, totalCount);
                foreach(int m in array)
                {
                    if (m == number)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    array[i] = number;
                    i++;
                }
            }
            return array;
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