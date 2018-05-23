namespace DI.Reminder.Common.CategoryModel
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
    }
}
