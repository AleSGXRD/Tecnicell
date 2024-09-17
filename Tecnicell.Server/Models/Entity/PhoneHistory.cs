using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class PhoneHistory
{
    public string Imei { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? ActionHistory { get; set; }

    public string? ToBranch { get; set; }

    public string? Description { get; set; }

    public string? SaleCode { get; set; }

    public virtual ActionHistory? ActionHistoryNavigation { get; set; }

    public virtual Phone ImeiNavigation { get; set; } = null!;

    public virtual Sale? SaleCodeNavigation { get; set; }

    public virtual Branch? ToBranchNavigation { get; set; }
}
