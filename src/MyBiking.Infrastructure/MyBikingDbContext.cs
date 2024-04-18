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
            //modelBuilder.Entity<IdentityRole>()
            //    .Ignore(c => c.ConcurrencyStamp)
            //    .Ignore(c => c.NormalizedName);

            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            //modelBuilder.Entity<ApplicationUser>().Ignore(c => c.AccessFailedCount)
            //                               .Ignore(c => c.LockoutEnabled)
            //                               .Ignore(c => c.TwoFactorEnabled)
            //                               .Ignore(c => c.EmailConfirmed)
            //                               .Ignore(c => c.ConcurrencyStamp)
            //                               .Ignore(c => c.LockoutEnd)
            //                               .Ignore(c => c.PhoneNumberConfirmed)
            //                               .Ignore(c => c.PhoneNumber);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOne(p => p.Nationality)
            //    .WithOne()
            //    .HasForeignKey<ApplicationUser>(p => p.NationalityId)
            //    .IsRequired(true);

            //modelBuilder.Entity<ApplicationUser>().ToTable("Users");


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
            //.HasForeignKey("RideId");

            modelBuilder.Entity<WheelieRide>()
                .HasMany(w => w.WheeleItems)
                .WithOne(p => p.WheelieRide)
                .HasForeignKey(p => p.WheelieRideId)
                .IsRequired(true);

            //.HasForeignKey("WheelieRideId");

            modelBuilder.Entity<WheelieItem>()
                .OwnsOne(p => p.WheelePoint);

            //modelBuilder.Entity<ApplicationUser>()
            //    .Ignore(u => u.PasswordHelpers);

            modelBuilder.Entity<Ride>()
                .HasOne(p => p.ApplicationUser)
                .WithMany(p => p.Rides)
                .HasForeignKey(p => p.ApplicationUserId)
                .IsRequired(true);

            //modelBuilder.Entity<User>()
            //    .HasKey(p=>p.Id);
            //modelBuilder.Entity<User>()
            //    .HasMany(p => p.Rides)
            //    .WithOne(p => p.User)
            //    .HasForeignKey(p => p.UserId)
            //    .IsRequired(true);
        }

    }
}
