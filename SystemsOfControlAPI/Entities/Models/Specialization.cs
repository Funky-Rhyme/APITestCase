﻿using System;
using System.Collections.Generic;

namespace SystemsOfControlAPI.Entities.Models;

public partial class Specialization
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
