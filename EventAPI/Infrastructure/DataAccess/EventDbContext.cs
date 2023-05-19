using EventAPI.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace EventAPI.Infrastructure.DataAccess
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
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(p => p.EventId);


            modelBuilder.Entity<Event>().HasData(
             new Event { Id = 1, Title = "Title 1", ContactPerson = "John Doe1", Description = "Description 1", Location="location 1", EndDateTime=DateTime.Now.AddDays(10), StartDateTime =  DateTime.Now, TimeZone = "timezone 1"},
             new Event { Id = 2, Title = "Title 2", ContactPerson = "John Doe2", Description = "Description 2", Location = "location 2", EndDateTime = DateTime.Now.AddDays(15), StartDateTime = DateTime.Now, TimeZone = "timezone 2" },
             new Event { Id = 3, Title = "Title 3", ContactPerson = "John Doe3", Description = "Description 3", Location = "location 3", EndDateTime = DateTime.Now.AddDays(20), StartDateTime = DateTime.Now, TimeZone = "timezone 1" },
             new Event { Id = 4, Title = "Title 4", ContactPerson = "John Doe3", Description = "Description 3", Location = "location 3", EndDateTime = DateTime.Now.AddDays(20), StartDateTime = DateTime.Now, TimeZone = "timezone 1" },
             new Event { Id = 5, Title = "Title 5", ContactPerson = "John Doe1", Description = "Description 1", Location = "location 1", EndDateTime = DateTime.Now.AddDays(10), StartDateTime = DateTime.Now, TimeZone = "timezone 1" },
             new Event { Id = 6, Title = "Title 6", ContactPerson = "John Doe2", Description = "Description 2", Location = "location 2", EndDateTime = DateTime.Now.AddDays(15), StartDateTime = DateTime.Now, TimeZone = "timezone 2" },
             new Event { Id = 7, Title = "Title 7", ContactPerson = "John Doe3", Description = "Description 3", Location = "location 3", EndDateTime = DateTime.Now.AddDays(20), StartDateTime = DateTime.Now, TimeZone = "timezone 1" },
             new Event { Id = 8, Title = "Title 8", ContactPerson = "John Doe3", Description = "Description 3", Location = "location 3", EndDateTime = DateTime.Now.AddDays(20), StartDateTime = DateTime.Now, TimeZone = "timezone 1" }
              );
            modelBuilder.Entity<Participant>().HasData(
             new Participant { Id = 1, EventId = 1, UserId = 1 },
             new Participant { Id = 2, EventId = 1, UserId = 2 },
             new Participant { Id = 3, EventId = 1, UserId = 3 }
             );

        }
    }
 
}
