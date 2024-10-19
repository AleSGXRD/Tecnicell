using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Battery
{
    public string BatteryCode { get; set; } = null!;

    public string? Name { get; set; }

    public string? Brand { get; set; }

    public decimal? SalePrice { get; set; }

    public string? ImageCode { get; set; }

    public int? Warranty { get; set; }

    public virtual ICollection<BatteryHistory> BatteryHistories { get; set; } = new List<BatteryHistory>();

    public virtual Brand? BrandNavigation { get; set; }

    public virtual Image? ImageCodeNavigation { get; set; }
}
