//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBiking.Entity.Models;

namespace MyBiking.Infrastructure
{
    public class MyBikingDbContext:IdentityDbContext<ApplicationUser>
    {


        public virtual DbSet<Ride> Rides { get; set; }
        public virtual DbSet<WheelieRide> WheelieRides { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<WheelieItem> WheelieItems { get; set; }
        public virtual DbSet<ApplicationUser> Users { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }

        public MyBikingDbContext()
        {

        }
        public MyBikingDbContext(DbContextOptions<MyBikingDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Ignore(p => p.Password);

            modelBuilder.Entity<Ride>()
                .HasMany(r => r.WheeleRides)
                .WithOne(p => p.Ride)
                .HasForeignKey(p => p.RideId)
                .IsRequired(true);

            modelBuilder.Entity<Ride>()
                .Property(p => p.IsPublic)
                .HasDefaultValue(false);

            modelBuilder.Entity<Ride>()
                .HasMany(r => r.Points)
                .WithOne(p => p.Ride)
                .HasForeignKey(p => p.RideId)
                .IsRequired(true);

            modelBuilder.Entity<WheelieRide>()
                .HasMany(w => w.WheeleItems)
                .WithOne(p => p.WheelieRide)
                .HasForeignKey(p => p.WheelieRideId)
                .IsRequired(true);

            modelBuilder.Entity<WheelieItem>()
                .OwnsOne(p => p.WheelePoint);

            modelBuilder.Entity<Ride>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(p => p.Rides)
                .HasForeignKey(p => p.ApplicationUserId)
                .IsRequired(true);
        }

    }
}
