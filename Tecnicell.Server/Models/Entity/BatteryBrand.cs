using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class BatteryBrand
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Battery> Batteries { get; set; } = new List<Battery>();
}
