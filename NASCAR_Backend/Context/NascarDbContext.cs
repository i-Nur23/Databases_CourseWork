using NASCAR_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace NASCAR_Backend.Context
{
    public class NascarDbContext : DbContext
    {
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Change> Changes { get; set; }
        public DbSet<Result> Results { get; set; }


        public NascarDbContext(DbContextOptions<NascarDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Pilot>().HasAlternateKey(u => u.CarsNumber);
            modelBuilder.Entity<Pilot>().HasCheckConstraint("PerformanceStatus", "PerformanceStatus IN ('OFF', 'ON', 'PT')");
            modelBuilder.Entity<Pilot>().Property(u => u.CarsNumber).HasDefaultValue(0);

            modelBuilder.Entity<Team>().HasAlternateKey(u => u.Name);

            modelBuilder.Entity<Manufacturer>().HasAlternateKey(u => u.Brand);
            modelBuilder.Entity<Manufacturer>().HasAlternateKey(u => u.Model);
            modelBuilder.Entity<Manufacturer>().HasCheckConstraint("Brand", "Brand IN ('Chevrolet', 'Ford', 'Toyota')");

            modelBuilder.Entity<Stage>().HasAlternateKey(u => u.Name);
            modelBuilder.Entity<Stage>().HasAlternateKey(u => u.EventsDate);

            modelBuilder.Entity<Track>().HasAlternateKey(u => u.Name);
            modelBuilder.Entity<Track>().HasCheckConstraint("TracksType", "TracksType IN ('SS', 'ST', 'RC')");

            modelBuilder.Entity<Result>().HasKey(u => new { u.PilotID, u.StageID });
        }


    }
}
