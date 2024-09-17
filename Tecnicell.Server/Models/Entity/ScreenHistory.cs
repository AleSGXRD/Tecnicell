using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class ScreenHistory
{
    public string ScreenCode { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? ActionHistory { get; set; }

    public string? ToBranch { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public string? SaleCode { get; set; }

    public virtual ActionHistory? ActionHistoryNavigation { get; set; }

    public virtual Sale? SaleCodeNavigation { get; set; }

    public virtual Screen ScreenCodeNavigation { get; set; } = null!;

    public virtual Branch? ToBranchNavigation { get; set; }
}
