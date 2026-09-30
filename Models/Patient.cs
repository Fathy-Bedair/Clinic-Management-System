using Clinic_Management_System.Validations;

namespace Clinic_Management_System.Models
{
    public enum PGender
    {
        Male,
        Female
    }
    public class Patient
    {
        public int Id { get; set; }
        [MinLength(7)]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public PGender Gender { get; set; }
        public string BloodType { get; set; } = string.Empty;
        [ValidEgyptianPhone]
        public string PhoneNumber { get; set; } = string.Empty;
        [ValidateNever]
        public List<Appointment> Appointments { get; set; }
        [ValidateNever]
        public List<MedicalRecord> MedicalRecords { get; set; }

    }
}
