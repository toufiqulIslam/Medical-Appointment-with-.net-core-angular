using MedicalAppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentAPI.Data {
    public class MedicalContext : DbContext {
        public MedicalContext(DbContextOptions<MedicalContext> options) : base(options) {}

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" },
                new Patient { PatientId = 2, Name = "Jane Smith", Email = "jane@example.com", Phone = "9876543210" }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 1, Name = "Dr. Strange", Specialty = "Cardiology" },
                new Doctor { DoctorId = 2, Name = "Dr. Watson", Specialty = "Dermatology" }
            );
        }
    }
}