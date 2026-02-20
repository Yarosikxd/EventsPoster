namespace EvensPoster.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int Score { get; set; } // 1-5

        public int EventId { get; set; }
        public Event Event { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
