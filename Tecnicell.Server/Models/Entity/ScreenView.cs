using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class ScreenView
{
    public string? Code { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    public decimal? SalePrice { get; set; }

    public int? Warranty { get; set; }

    public string? ImageCode { get; set; }

    public long? Quantity { get; set; }

    public string? Available { get; set; }
}
