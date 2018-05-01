using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DI.Reminder.BL
{
    public class GetPrompts : IPrompt
    {
        public List<Prompt> _list = new List<Prompt>
        {
            new Prompt{ID=1,Name="Выгулить собаку", Category="Important",  Description="Здесь какое-то подробное описание", CreatingDate= new DateTime(2015,4,5,12,3,20)},
            new Prompt{ID=2,Name="Съездить за договором", Category="Work",  Description="Здесь какое-то подробное описание" ,CreatingDate= new DateTime(2018,4,10,4,56,20)},
            new Prompt{ID=3,Name="Купить изоленту", Category="Others",  Description="Здесь какое-то подробное описание",CreatingDate= new DateTime(2005,10,5,2,9,20) },
            new Prompt{ID=4,Name="Сделать себе подарок", Category="Personal",  Description="Здесь какое-то подробное описание",CreatingDate= new DateTime(2019,1,2,7,35,20) },
            new Prompt{ID=5,Name="Позвонить заказчику", Category="Work",  Description="Здесь какое-то подробное описание" ,CreatingDate= new DateTime(2018,4,6,9,58,20)},
            new Prompt{ID=6,Name="Найти смысл жизни", Category="Other",  Description="Здесь какое-то подробное описание",CreatingDate= new DateTime(2018,1,12,5,23,55) },
            new Prompt{ID=7,Name="Найти хобби", Category="Personal",  Description="Здесь какое-то подробное описание",CreatingDate= new DateTime(2012,5,6,12,36,20) }

        };
        public IEnumerable<Prompt> GetList(string _category)
        {
            if (_category == "All")
                return _list;
            return _list.FindAll(f => f.Category == _category);
        }
        public Prompt GetPrompt(int? _id)
        {
            return _list.FirstOrDefault(f => f.ID == _id);
        }
    }
}
