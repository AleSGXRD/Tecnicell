using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class PhoneView
{
    public string? Code { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    public decimal? SalePrice { get; set; }

    public string? ImageCode { get; set; }

    public decimal? Cost { get; set; }

    public string? CurrentState { get; set; }

    public string? ActionDescription { get; set; }
}
