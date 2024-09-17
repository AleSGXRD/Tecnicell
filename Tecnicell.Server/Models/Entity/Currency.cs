using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Currency
{
    public string CurrencyCode { get; set; } = null!;

    public string? CurrencyName { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
