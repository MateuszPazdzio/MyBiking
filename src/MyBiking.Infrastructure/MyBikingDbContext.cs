//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBiking.Application.Models;

namespace MyBiking.Infrastructure
{
    public class MyBikingDbContext:IdentityDbContext<User>
    {


        public virtual DbSet<Ride> Rides { get; set; }
        public virtual DbSet<WheelieRide> WheelieRides { get; set; }
        public virtual DbSet<Point> Points{ get; set; }
        public virtual DbSet<WheelieItem> WheelieItems{ get; set; }
        public virtual DbSet<User> Users { get; set; }
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

            //var x = modelBuilder.Model.ToDebugString();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Ignore(c => c.AccessFailedCount)
                                           .Ignore(c => c.LockoutEnabled)
                                           .Ignore(c => c.TwoFactorEnabled)
                                           .Ignore(c => c.EmailConfirmed)
                                           .Ignore(c => c.ConcurrencyStamp)
                                           .Ignore(c => c.LockoutEnd)
                                           .Ignore(c => c.PhoneNumberConfirmed)
                                           .Ignore(c => c.PhoneNumber);

            modelBuilder.Entity<User>().ToTable("Users");//to change the name of table.


            modelBuilder.Entity<Ride>()
                .HasMany(r=>r.WheeleRides)
                .WithOne(p => p.Ride)
                .HasForeignKey(p => p.RideId)
                .IsRequired(true);

            modelBuilder.Entity<Ride>()
                .HasMany(r=>r.Points)
                .WithOne(p => p.Ride)
                .HasForeignKey(p => p.RideId)
                .IsRequired(true);
            //.HasForeignKey("RideId");

            modelBuilder.Entity<WheelieRide>()
                .HasMany(w=>w.WheeleItems)
                .WithOne(p => p.WheelieRide)
                .HasForeignKey(p => p.WheelieRideId)
                .IsRequired(true);

            //.HasForeignKey("WheelieRideId");

            modelBuilder.Entity<WheelieItem>()
                .OwnsOne(p => p.WheelePoint);

            modelBuilder.Entity<User>()
                .Ignore(u => u.PasswordHelpers);

            modelBuilder.Entity<Ride>()
                .HasOne(p=>p.User)
                .WithMany(p=>p.Rides)
                .HasForeignKey(p=>p.UserId)
                .IsRequired(true);

            //modelBuilder.Entity<User>()
            //    .HasKey(p=>p.Id);
            //modelBuilder.Entity<User>()
            //    .HasMany(p => p.Rides)
            //    .WithOne(p => p.User)
            //    .HasForeignKey(p => p.UserId)
            //    .IsRequired(true);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(@"Server=(localdb)\\MSSQLLocalDB;Database=MyBiking;Trusted_Connection=True;TrustServerCertificate=True;");
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-15VL6MS\SQLEXPRESS;Database=MyBiking;Trusted_Connection=True;TrustServerCertificate=True;");
        //}
    }
}
