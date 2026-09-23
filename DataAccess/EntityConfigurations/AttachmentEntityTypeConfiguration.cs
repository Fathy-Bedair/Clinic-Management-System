using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_System.DataAccess.EntityConfigurations
{
    public class AttachmentEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FileName)
                .HasColumnType("nvarchar(200)");
            builder.Property(a => a.FilePath)
                .HasColumnType("nvarchar(500)");
            builder.Property(a => a.FileType)
                .HasColumnType("nvarchar(100)");
            builder.HasOne(a => a.MedicalRecord)
                .WithMany(mr => mr.Attachments)
                .HasForeignKey(a => a.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
