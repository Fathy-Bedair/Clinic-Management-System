using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Clinic_Management_System.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Description { get; set; }
        [ValidateNever]
        public List<Doctor> Doctors { get; set; }
    }
}
