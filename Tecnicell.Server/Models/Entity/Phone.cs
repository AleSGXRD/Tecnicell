using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Phone
{
    public string Imei { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Name { get; set; }

    public decimal? SalePrice { get; set; }

    public string? ImageCode { get; set; }

    public virtual Brand? BrandNavigation { get; set; }

    public virtual Image? ImageCodeNavigation { get; set; }

    public virtual ICollection<PhoneHistory> PhoneHistories { get; set; } = new List<PhoneHistory>();
}
