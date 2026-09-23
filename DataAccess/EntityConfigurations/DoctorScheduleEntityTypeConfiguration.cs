using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_System.DataAccess.EntityConfigurations
{
    public class DoctorScheduleEntityTypeConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.HasKey(ds => ds.Id);
            builder.Property(ds => ds.Day)
                .IsRequired();
            builder.Property(ds => ds.StartTime)
                .IsRequired();
            builder.Property(ds => ds.EndTime)
                .IsRequired();
            builder.Property(ds => ds.SlotDurationMinutes)
                .IsRequired()
                .HasDefaultValue(30);
            builder.HasOne(ds => ds.Doctor)
                .WithMany(d => d.Schedules)
                .HasForeignKey(ds => ds.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
