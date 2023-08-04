using System;
using System.Collections.Generic;

namespace SystemsOfControlAPI.Entities.Models;

public partial class District
{
    public int Number { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
