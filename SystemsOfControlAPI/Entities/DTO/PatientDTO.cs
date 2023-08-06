using System.ComponentModel.DataAnnotations;

namespace SystemsOfControlAPI.Entities.DTO
{
    public class PatientDTO
    {
        public string Surname { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }
        [MaxLength(1)]
        public string Sex { get; set; } = null!;
        [Range(1, double.MaxValue)]
        public int District { get; set; }
    }
}
