namespace Clinic_Management_System.Models
{
    public enum Status
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed,
        NoShow
    }
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public Status Status { get; set; }
        public string? ReasonForVisit { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
    }
}
