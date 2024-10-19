using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class PhoneRepair
{
    public string Imei { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Name { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerId { get; set; }

    public string? CustomerNumber { get; set; }

    public string? ImageCode { get; set; }

    public decimal? Price { get; set; }

    public virtual Brand? BrandNavigation { get; set; }

    public virtual Image? ImageCodeNavigation { get; set; }

    public virtual ICollection<PhoneRepairHistory> PhoneRepairHistories { get; set; } = new List<PhoneRepairHistory>();
}
