using Clinic_Management_System.Validations;

namespace Clinic_Management_System.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int SpecializationId { get; set; }
        [ValidateNever]
        public Specialization Specialization { get; set; } = null!;
        [MinLength(7)]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [ValidEgyptianPhone]
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal ConsultationFee { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsActive { get; set; }
        [ValidateNever]
        public List<DoctorSchedule> Schedules { get; set; }
        [ValidateNever]
        public List<Appointment> Appointments { get; set; }
        [ValidateNever]
        public List<MedicalRecord> MedicalRecords { get; set; }
    }
}
