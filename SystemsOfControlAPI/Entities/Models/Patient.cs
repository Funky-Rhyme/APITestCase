using System;
using System.Collections.Generic;

namespace SystemsOfControlAPI.Entities.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Sex { get; set; } = null!;

    public int District { get; set; }

    public virtual District DistrictNavigation { get; set; } = null!;
}
