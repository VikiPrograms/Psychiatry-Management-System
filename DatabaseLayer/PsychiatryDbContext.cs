using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace DatabaseLayer
{
    public class PsychiatryDbContext : IdentityDbContext<User>
    {
        public PsychiatryDbContext() { }
        public PsychiatryDbContext(DbContext dbContext) { }

        public PsychiatryDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=PC\\SQLEXPRESS;Database=Victory_Co;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();

            modelBuilder.Entity<Patient>()
            .Property(p => p.AdmissionDate)
            .HasConversion(new DateOnlyConverter())
            .HasColumnType("date")
            .IsRequired();

            modelBuilder.Entity<Patient>()
            .Property(p => p.Checkout)
            .HasConversion(new DateOnlyConverter())
            .HasColumnType("date")
            .IsRequired();

            modelBuilder.Entity<Medication>()
            .Property(m => m.Cost)
            .HasColumnType("decimal(18, 2)");

            base.OnModelCreating(modelBuilder);
        }

        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter() : base(
                dateOnly => DateTime.Parse(dateOnly.ToString("yyyy-MM-dd")),
                dateTime => DateOnly.FromDateTime(dateTime.Date))
            {
            }
        }

        public DbSet<Medication> Medications { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
