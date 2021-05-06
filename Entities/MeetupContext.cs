using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupAPI.Entities
{
    public class MeetupContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=MeetupDb;Trusted_Connection=True;";

        public DbSet<Meetup> Meetups { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Lecture> Lectures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meetup>()
                .HasOne(m => m.Location)
                .WithOne(n => n.Meetup)
                .HasForeignKey<Location>(n => n.MeetupId);

            modelBuilder.Entity<Meetup>()
                .HasMany(m => m.Lectures)
                .WithOne(n => n.Meetup);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
