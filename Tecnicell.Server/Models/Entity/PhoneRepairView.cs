using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class PhoneRepairView
{
    public string? Code { get; set; }

    public string? Type { get; set; }

    public string? Name { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerId { get; set; }

    public string? CustomerNumber { get; set; }

    public decimal? Price { get; set; }

    public string? ImageCode { get; set; }

    public DateTime? Date { get; set; }

    public string? CurrentState { get; set; }

    public string? ActionDescription { get; set; }
}
