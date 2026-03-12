using EvensPoster.Roles;
using System.ComponentModel.DataAnnotations;

namespace EvensPoster.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? City { get; set; }

        public string? About { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRole Role { get; set; }

        // Навігаційні властивості
        public List<Event>? OrganizedEvents { get; set; }
        public List<EventParticipant>? EventParticipants { get; set; }
        public List<EventSponsor>? EventSponsors { get; set; }
        public List<EventVisitor>? EventVisitors { get; set; }
    }
}