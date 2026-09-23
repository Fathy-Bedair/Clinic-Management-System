using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_System.DataAccess.EntityConfigurations
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AppointmentDate)
                .IsRequired();
            builder.Property(a => a.AppointmentTime)
                .IsRequired();
            builder.Property(a => a.Status)
                .IsRequired();
            builder.Property(a => a.ReasonForVisit)
                .HasColumnType("nvarchar(500)");
            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(a => new { a.DoctorId, a.AppointmentDate, a.AppointmentTime })
                .IsUnique();
        }
    }
}


