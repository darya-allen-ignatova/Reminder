using System.ComponentModel.DataAnnotations;

namespace DI.Reminder.Common.CategoryModel
{
    public class CategoriesModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string ParentCategory { get; set; }
    }
}
