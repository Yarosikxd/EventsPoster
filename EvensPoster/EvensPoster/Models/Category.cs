namespace EvensPoster.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<EventCategory>? EventCategories { get; set; }
    }
}
