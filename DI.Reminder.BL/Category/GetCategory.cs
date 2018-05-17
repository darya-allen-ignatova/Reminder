using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Data;

namespace DI.Reminder.BL.Category
{
    public class GetCategory:IGetCategory
    {
        IGetData _getData;
        public GetCategory(IGetData getData)
        {
            _getData = getData;
        }
        public IList<Category> Get(int? id)
        {
            IList<DataCategory> datalist = _getData.GetCategories(id);
            List<Category> _list = new List<Category>();
            for (int i = 0; i < datalist.Count; i++)
                _list.Add(new Category { ID = datalist[i].ID, Name = datalist[i].Name, ParentID = datalist[i].ParentID });
            return _list;
        }
    }
}
