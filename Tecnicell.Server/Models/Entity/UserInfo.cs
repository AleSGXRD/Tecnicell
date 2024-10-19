using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class UserInfo
{
    public string UserCode { get; set; } = null!;

    public string? Role { get; set; }

    public string? Branch { get; set; }

    public string? ImageCode { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AccessoryHistory> AccessoryHistories { get; set; } = new List<AccessoryHistory>();

    public virtual ICollection<BatteryHistory> BatteryHistories { get; set; } = new List<BatteryHistory>();

    public virtual Branch? BranchNavigation { get; set; }

    public virtual Image? ImageCodeNavigation { get; set; }

    public virtual ICollection<PhoneHistory> PhoneHistories { get; set; } = new List<PhoneHistory>();

    public virtual ICollection<PhoneRepairHistory> PhoneRepairHistories { get; set; } = new List<PhoneRepairHistory>();

    public virtual Role? RoleNavigation { get; set; }

    public virtual ICollection<ScreenHistory> ScreenHistories { get; set; } = new List<ScreenHistory>();

    public virtual UserAccount? UserAccount { get; set; }
}
