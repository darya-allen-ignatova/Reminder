using System.ComponentModel.DataAnnotations;

namespace DI.Reminder.Common.CategoryModel
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public Category ParentCategory { get; set; }
    }
}
