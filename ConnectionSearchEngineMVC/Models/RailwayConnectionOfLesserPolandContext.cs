using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConnectionSearchEngineMVC.Models
{
    public partial class RailwayConnectionOfLesserPolandContext : DbContext
    {
        public virtual DbSet<Ska1KrkWiel> Ska1KrkWiel { get; set; }
        public virtual DbSet<Ska1WielKrk> Ska1WielKrk { get; set; }
        public virtual DbSet<Ska2KrkSed> Ska2KrkSed { get; set; }
        public virtual DbSet<Ska2SedKrk> Ska2SedKrk { get; set; }
        public virtual DbSet<Ska3KrkTar> Ska3KrkTar { get; set; }
        public virtual DbSet<Ska3TarKrk> Ska3TarKrk { get; set; }
        public virtual DbSet<ReservationRegister> ReservationRegister { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=USER-KOMPUTER\SQLEXPRESS;Database=RailwayConnectionOfLesserPoland;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ska1KrkWiel>(entity =>
            {
                entity.ToTable("SKA1_KRK_WIEL");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Train)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ska1WielKrk>(entity =>
            {
                entity.ToTable("SKA1_WIEL_KRK");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Train)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ska2KrkSed>(entity =>
            {
                entity.ToTable("SKA2_KRK_SED");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Train)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ska2SedKrk>(entity =>
            {
                entity.ToTable("SKA2_SED_KRK");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Train)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ska3KrkTar>(entity =>
            {
                entity.ToTable("SKA3_KRK_TAR");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Train)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ska3TarKrk>(entity =>
            {
                entity.ToTable("SKA3_TAR_KRK");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Station)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Train)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReservationRegister>(entity =>
            {
                entity.ToTable("ReservationRegister");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TypeOfTicket)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstStation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SecondStation)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Train)
                    .IsRequired()
                    .HasMaxLength(50);

            });
        }
    }
}
