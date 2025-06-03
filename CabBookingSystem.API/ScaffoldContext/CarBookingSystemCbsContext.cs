using System;
using System.Collections.Generic;
using CabBookingSystem.API.ScaffoldModels;
using Microsoft.EntityFrameworkCore;

namespace CabBookingSystem.API.ScaffoldContext;

public partial class CarBookingSystemCbsContext : DbContext
{
    public CarBookingSystemCbsContext()
    {
    }

    public CarBookingSystemCbsContext(DbContextOptions<CarBookingSystemCbsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Ride> Rides { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LTIN620456\\SQLEXPRESS;Initial Catalog=CarBookingSystemCBS;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Drivers__F1B1CD24FC59A442");

            entity.HasIndex(e => e.LicenseNumber, "UQ__Drivers__E88901660950BB99").IsUnique();

            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.LicenseNumber).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Available");
            entity.Property(e => e.VehicleDetails).HasMaxLength(255);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A583EE79CDF");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.RideId).HasColumnName("RideID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Ride).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__RideID__45F365D3");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__UserID__46E78A0C");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF85C2427AF99");

            entity.Property(e => e.RatingId).HasColumnName("RatingID");
            entity.Property(e => e.Comments).HasMaxLength(1000);
            entity.Property(e => e.FromUserId).HasColumnName("FromUserID");
            entity.Property(e => e.RideId).HasColumnName("RideID");
            entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

            entity.HasOne(d => d.FromUser).WithMany(p => p.RatingFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ratings__FromUse__4BAC3F29");

            entity.HasOne(d => d.Ride).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.RideId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ratings__RideID__4AB81AF0");

            entity.HasOne(d => d.ToUser).WithMany(p => p.RatingToUsers)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ratings__ToUserI__4CA06362");
        });

        modelBuilder.Entity<Ride>(entity =>
        {
            entity.HasKey(e => e.RideId).HasName("PK__Rides__C5B8C414101950A7");

            entity.Property(e => e.RideId).HasColumnName("RideID");
            entity.Property(e => e.DriverId).HasColumnName("DriverID");
            entity.Property(e => e.DropoffLocation).HasMaxLength(255);
            entity.Property(e => e.Fare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PickupLocation).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Driver).WithMany(p => p.Rides)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rides__DriverID__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Rides)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rides__UserID__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC14C300BF");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105343F4A3DB4").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
