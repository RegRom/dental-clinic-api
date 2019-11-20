using DentalClinicDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinicDAL
{
    public class DentalClinicContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        public DentalClinicContext(DbContextOptions<DentalClinicContext> options)
            : base(options)
        {
        }
    }
}
