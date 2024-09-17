using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Accessory
{
    public string AccessoryCode { get; set; } = null!;

    public string? Name { get; set; }

    public string? AccessoryType { get; set; }

    public decimal? SalePrice { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<AccessoryHistory> AccessoryHistories { get; set; } = new List<AccessoryHistory>();

    public virtual AccessoryType? AccessoryTypeNavigation { get; set; }
}
