using System;
using System.Collections.Generic;

namespace SystemsOfControlAPI.Entities.Models;

public partial class Doctor
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public int Cabinet { get; set; }

    public int Specialization { get; set; }

    public int? District { get; set; }

    public virtual Cabinet CabinetNavigation { get; set; } = null!;

    public virtual District? DistrictNavigation { get; set; }

    public virtual Specialization SpecializationNavigation { get; set; } = null!;
}
