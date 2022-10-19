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
            modelBuilder.Entity<Pilot>().HasCheckConstraint("CarsNumber", "CarsNumber >= 0 AND CarsNumber <= 99");
            modelBuilder.Entity<Pilot>().HasCheckConstraint("Points", "Points >= 0");

            modelBuilder.Entity<Team>().HasAlternateKey(u => u.Name);
            modelBuilder.Entity<Team>().HasCheckConstraint("FoundationYear", "FoundationYear >= 1900 AND FoundationYear <= 2021");

            modelBuilder.Entity<Manufacturer>().HasAlternateKey(u => u.Brand);
            modelBuilder.Entity<Manufacturer>().HasAlternateKey(u => u.Model);
            modelBuilder.Entity<Manufacturer>().HasCheckConstraint("Brand", "Brand IN ('Chevrolet', 'Ford', 'Toyota')");

            modelBuilder.Entity<Stage>().HasAlternateKey(u => u.Name);
            modelBuilder.Entity<Stage>().HasAlternateKey(u => u.EventsDate);

            modelBuilder.Entity<Track>().HasAlternateKey(u => u.Name);
            modelBuilder.Entity<Track>().HasCheckConstraint("TracksType", "TracksType IN ('SS', 'ST', 'RC')");
            modelBuilder.Entity<Track>().HasCheckConstraint("Length", "Length > 0");

            modelBuilder.Entity<Change>().HasCheckConstraint("OldNumber", "OldNumber >= 0 AND OldNumber <= 99");
            modelBuilder.Entity<Change>().HasCheckConstraint("NewNumber", "NewNumber >= 0 AND NewNumber <= 99");

            modelBuilder.Entity<Result>().HasKey(u => new { u.PilotID, u.StageID });
            modelBuilder.Entity<Result>().HasCheckConstraint("Place", "Place >= 1");
            modelBuilder.Entity<Result>().HasCheckConstraint("LeaderGap", "LeaderGap >= 0");
            modelBuilder.Entity<Result>().HasCheckConstraint("NumberOfPitStops", "NumberOfPitStops >= 0");

        }


    }
}
