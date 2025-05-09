using Microsoft.EntityFrameworkCore;

namespace WaypointsApi.Models
{
    public class WaypointsDbContext : DbContext
    {
        public WaypointsDbContext(DbContextOptions<WaypointsDbContext> options) : base(options) { }

        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BorderPoint> BorderPoints { get; set; }
        public DbSet<WaypointSettings> WaypointSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Waypoint>()
                .HasOne(w => w.Location)
                .WithOne()
                .HasForeignKey<Waypoint>(w => w.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Waypoint>()
                .HasMany(w => w.BorderPoints)
                .WithOne()
                .HasForeignKey(bp => bp.WaypointId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed initial settings
            modelBuilder.Entity<WaypointSettings>().HasData(new WaypointSettings
            {
                Id = 1,
                DefaultEnterDistance = 30,
                DefaultExitDistance = 50,
                DefaultMaxEnteringDeviationAngle = 45
            });

            // Seed initial location
            modelBuilder.Entity<Location>().HasData(new Location
            {
                Id = 1,
                Latitude = 58.1,
                Longitude = 10.9
            });

            // Seed initial waypoint
            modelBuilder.Entity<Waypoint>().HasData(new Waypoint
            {
                Id = 1,
                Ref = "9025002000000111",
                Extra = "Extra",
                Name = "Nösnäs",
                Gate = "B",
                Direction = 90,
                EnterDistance = 35,
                ExitDistance = 10,
                Length = 30,
                LocationId = 1
            });

            // Seed initial border points
            modelBuilder.Entity<BorderPoint>().HasData(
                new BorderPoint { Id = 1, Latitude = 58.1, Longitude = 10.9, WaypointId = 1 },
                new BorderPoint { Id = 2, Latitude = 58.2, Longitude = 10.8, WaypointId = 1 },
                new BorderPoint { Id = 3, Latitude = 58.3, Longitude = 10.7, WaypointId = 1 }
            );
        }
    }
} 