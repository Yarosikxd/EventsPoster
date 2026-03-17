using System.ComponentModel.DataAnnotations;

namespace EvensPoster.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string ImageURL { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? City { get; set; }

        public decimal? Price { get; set; }

        public bool IsFinished { get; set; }

        // Організатор
        public int OrganizerId { get; set; }
        public User Organizer { get; set; }


        // Зв’язки
        public List<EventCategory>? EventCategories { get; set; }
        public List<EventParticipant>? Participants { get; set; }
        public List<EventSponsor>? Sponsors { get; set; }
        public List<EventVisitor>? Visitors { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Rating>? Ratings { get; set; }

    }
}
