using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Screen
{
    public string ScreenCode { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public decimal? Width { get; set; }

    public decimal? Height { get; set; }

    public decimal? SalePrice { get; set; }

    public int? Warranty { get; set; }

    public virtual PhoneBrand? BrandNavigation { get; set; }

    public virtual ICollection<ScreenHistory> ScreenHistories { get; set; } = new List<ScreenHistory>();
}
