using System.ComponentModel.DataAnnotations;

namespace DI.Reminder.Common.CategoryModel
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        public string Name { get; set; }
        
        public int? ParentID { get; set; }
    }
}
