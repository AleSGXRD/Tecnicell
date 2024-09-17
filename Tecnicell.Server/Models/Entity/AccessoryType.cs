using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class AccessoryType
{
    public string AccessoryTypeCode { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();
}
