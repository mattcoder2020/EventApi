using EventAPI.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Infrastructure.DB
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public EventDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure One-to-many relationship between Event and Participant
             modelBuilder.Entity<Participant>()
                .HasOne(p => p.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(p => p.EventId);
         }
    }
}
