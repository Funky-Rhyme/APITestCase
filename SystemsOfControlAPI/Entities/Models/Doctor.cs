using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SystemsOfControlAPI.Entities.Models;

public partial class Doctor
{
    [Key]
    [JsonIgnore]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? FullName { get; set; }

    public int? Cabinet { get; set; }

    public string? Specialization { get; set; }

    public int? District { get; set; }

    public virtual Cabinet? CabinetNavigation { get; set; }

    public virtual District? DistrictNavigation { get; set; }

    public virtual Specialization? SpecializationNavigation { get; set; }
}
