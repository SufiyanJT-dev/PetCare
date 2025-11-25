using Microsoft.EntityFrameworkCore;
using PatCareManagement.Domain.Entity;
using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PatCareManagement.Infrastucture.Persistance.Data
{
    public class PetCareDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=PetCareDb;User Id=sa;Password=Sufiyan@123;TrustServerCertificate=True;");

        }
        DbSet<Domain.Entity.User> Users { get; set; }

        public DbSet<Pets> Pets { get; set; }
        public DbSet<WeightHistory> WeightHistories { get; set; }
        public DbSet<MedicalEvent> MedicalEvents { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
