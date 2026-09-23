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
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public PGender Gender { get; set; }
        public string BloodType { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Appointment> Appointments { get; set; }
        public List<MedicalRecord> MedicalRecords { get; set; }

    }
}
