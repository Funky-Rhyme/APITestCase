using System.ComponentModel.DataAnnotations;

namespace SystemsOfControlAPI.Entities.DTO
{
    public class DoctorDTO
    {
        public string FullName { get; set; } = null!;
        [Range(1, double.MaxValue)]
        public string Cabinet { get; set; }
        [Range(1, double.MaxValue)]
        public string Specialization { get; set; }
        [Range(1, double.MaxValue)]
        public string? District { get; set; }
    }
}
