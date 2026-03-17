using System.ComponentModel.DataAnnotations;

namespace EvensPoster.ViewsModels
{
    public class EventCreateViewModel
    {
        [Required(ErrorMessage = "Назва обов'язкова")]
        public string Title { get; set; }

        public string? ImageUrl { get; set; } 

        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public string? City { get; set; }

        public decimal? Price { get; set; }
    }
}
