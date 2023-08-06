using System;
using System.Collections.Generic;

namespace SystemsOfControlAPI.Entities.Models;

public partial class Cabinet
{
    public int Id { get; set; }

    public int Number { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
