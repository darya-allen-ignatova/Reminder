using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Reminder.Data.DataBase;
using DI.Reminder.Data;

namespace DI.Reminder.BL.Repository
{
    public class PromptRepository:IPromptRepository
    {
        List<Prompt> _list;
        IGetData _getdata;
        public PromptRepository(IGetData getdata)
        {
            _getdata = getdata;
            _list = new List<Prompt>();
        }
        public IList<Prompt> GetCategoryItemsByID(int? id)
        {
            AppDatabase appd = new AppDatabase();
            var list=appd.GetItems(id);
            if (list == null)
                return null;
            for(int i=0; i<list.Count; i++)
            {
                _list.Add(new Prompt()
                {
                    ID = list[i].ID,
                  Name = list[i].Name,
                  Category=list[i].Category
                });
            }
            return _list;
        }
        public Prompt GetPromptDetails(int id)
        {
            DataPrompt dataprompt =_getdata.GetPrompt(id);
            Prompt prompt = new Prompt()
            {
                ID = dataprompt.ID,
                Name = dataprompt.Name,
                Description = dataprompt.Description,
                TimeOfPrompt = dataprompt.TimeOfPrompt,
                CreatingDate = dataprompt.CreatingDate,
                Category = dataprompt.Category,
                Image = dataprompt.Image
            };
            return prompt;
        }
        public int? GetID(string name)
        {
            return _getdata.GetCategoryID(name);
        }

    
    }
}
