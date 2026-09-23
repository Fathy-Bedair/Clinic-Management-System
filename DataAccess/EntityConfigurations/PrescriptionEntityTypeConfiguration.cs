using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_System.DataAccess.EntityConfigurations
{
    public class PrescriptionEntityTypeConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.MedicineName)
                .HasColumnType("nvarchar(200)");
            builder.Property(p => p.Dosage)
                .HasColumnType("nvarchar(100)");
            builder.Property(p => p.Frequency)
                .HasColumnType("nvarchar(100)");
            builder.HasOne(p => p.MedicalRecord)
                .WithMany(mr => mr.Prescriptions)
                .HasForeignKey(p => p.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
