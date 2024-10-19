using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Brand
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Battery> Batteries { get; set; } = new List<Battery>();

    public virtual ICollection<PhoneRepair> PhoneRepairs { get; set; } = new List<PhoneRepair>();

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    public virtual ICollection<Screen> Screens { get; set; } = new List<Screen>();
}
