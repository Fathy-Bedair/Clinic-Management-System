using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_System.DataAccess.EntityConfigurations
{
    public class DoctorEntityTypeConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(d => d.PhoneNumber).IsRequired().HasColumnType("nvarchar(15)");
            builder.Property(d => d.ConsultationFee).HasColumnType("decimal(18,2)");
            builder.Property(d => d.YearsOfExperience).IsRequired();
            builder.Property(d => d.IsActive).IsRequired();
            builder.HasOne(d => d.Specialization).WithMany(s => s.Doctors).HasForeignKey(d => d.SpecializationId);
        }
    }
}
