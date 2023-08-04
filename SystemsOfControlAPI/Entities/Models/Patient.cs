using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SystemsOfControlAPI.Entities.Models;

public partial class Patient
{
    [JsonIgnore]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? Name { get; set; }

    public string? MiddleName { get; set; }

    public string? Adress { get; set; }

    public DateTime? DateOfBirth { get; set; }
    [MaxLength(1)]
    public string? Sex { get; set; }

    public int? District { get; set; }

    [JsonIgnore]
    public virtual District? DistrictNavigation { get; set; }
}
