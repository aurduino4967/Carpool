using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Carpool.Models
{
    public partial class CarpoolDBContext : DbContext
    {
        public CarpoolDBContext()
        {
        }

        public CarpoolDBContext(DbContextOptions<CarpoolDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookedRide> BookedRides { get; set; } = null!;
        public virtual DbSet<OfferedRide> OfferedRides { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CarpoolDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookedRide>(entity =>
            {
                entity.HasKey(e => e.OfferId)
                    .HasName("PK__Booked_R__03D37AC29B9BAFEF");

                entity.ToTable("Booked_Rides");

                entity.Property(e => e.OfferId)
                    .HasMaxLength(400)
                    .HasColumnName("offer_id")
                    .IsFixedLength();

                entity.Property(e => e.BookedBy)
                    .HasMaxLength(320)
                    .HasColumnName("booked_by")
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Seats).HasColumnName("seats");

                entity.HasOne(d => d.BookedByNavigation)
                    .WithMany(p => p.BookedRides)
                    .HasForeignKey(d => d.BookedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booked_Ri__booke__2E1BDC42");

                entity.HasOne(d => d.Offer)
                    .WithOne(p => p.BookedRide)
                    .HasForeignKey<BookedRide>(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booked_Ri__offer__2F10007B");
            });

            modelBuilder.Entity<OfferedRide>(entity =>
            {
                entity.HasKey(e => e.OfferId)
                    .HasName("PK__Offer_Ri__03D37AC227BCD10B");

                entity.ToTable("Offered_Rides");

                entity.Property(e => e.OfferId)
                    .HasMaxLength(400)
                    .HasColumnName("offer_id")
                    .IsFixedLength();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.FromPlace)
                    .HasMaxLength(150)
                    .HasColumnName("from_place")
                    .IsFixedLength();

                entity.Property(e => e.OfferedBy)
                    .HasMaxLength(320)
                    .HasColumnName("offered_by")
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Seats).HasColumnName("seats");

                entity.Property(e => e.Stops)
                    .HasMaxLength(2000)
                    .HasColumnName("stops")
                    .IsFixedLength();

                entity.Property(e => e.Time)
                    .HasMaxLength(10)
                    .HasColumnName("time")
                    .IsFixedLength();

                entity.Property(e => e.ToPlace)
                    .HasMaxLength(150)
                    .HasColumnName("to_place")
                    .IsFixedLength();

                entity.HasOne(d => d.OfferedByNavigation)
                    .WithMany(p => p.OfferedRides)
                    .HasForeignKey(d => d.OfferedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Offer_Rid__offer__2B3F6F97");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uname)
                    .HasName("PK__User_Rel__C7D2484FB01EA284");

                entity.Property(e => e.Uname)
                    .HasMaxLength(320)
                    .HasColumnName("uname")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
