namespace Clinic_Management_System.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string phoneNumber { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsActive { get; set; }
        public List<DoctorSchedule> Schedules { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<MedicalRecord> MedicalRecords { get; set; }
    }
}
