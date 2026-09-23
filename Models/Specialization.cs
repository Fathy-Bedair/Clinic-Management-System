namespace Clinic_Management_System.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
