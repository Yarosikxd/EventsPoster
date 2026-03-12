using EvensPoster.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EvensPoster.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<EventSponsor> EventSponsors { get; set; }
        public DbSet<EventVisitor> EventVisitors { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --------------------------
            // EventCategory як many-to-many між Event та Category
            modelBuilder.Entity<EventCategory>()
                .HasKey(ec => new { ec.EventId, ec.CategoryId });

            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCategories)
                .HasForeignKey(ec => ec.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Category)
                .WithMany(c => c.EventCategories)
                .HasForeignKey(ec => ec.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // Event ↔ Organizer (User)
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(u => u.OrganizedEvents)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // EventParticipant ↔ Event та User
            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.User)
                .WithMany(u => u.EventParticipants)
                .HasForeignKey(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // EventSponsor ↔ Event та User
            modelBuilder.Entity<EventSponsor>()
                .HasOne(es => es.Event)
                .WithMany(e => e.Sponsors)
                .HasForeignKey(es => es.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventSponsor>()
                .HasOne(es => es.User)
                .WithMany(u => u.EventSponsors)
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // EventVisitor ↔ Event та User
            modelBuilder.Entity<EventVisitor>()
                .HasOne(ev => ev.Event)
                .WithMany(e => e.Visitors)
                .HasForeignKey(ev => ev.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventVisitor>()
                .HasOne(ev => ev.User)
                .WithMany(u => u.EventVisitors)
                .HasForeignKey(ev => ev.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // Comment ↔ Event та User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // Rating ↔ Event та User
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Ratings)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------
            // На всякий випадок встановлюємо Restrict для всіх FK
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}