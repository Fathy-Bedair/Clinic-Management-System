namespace Clinic_Management_System.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string? Symptoms { get; set; }
        public string? Notes { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
