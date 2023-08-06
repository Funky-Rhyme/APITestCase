using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SystemsOfControlAPI.Entities.Models;

public partial class Patient
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
    [MaxLength(1)]
    public string Sex { get; set; } = null!;

    public int District { get; set; }
    [JsonIgnore]
    public virtual District DistrictNavigation { get; set; } = null!;
}
