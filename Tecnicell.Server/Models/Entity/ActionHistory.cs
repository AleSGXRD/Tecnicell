﻿using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class ActionHistory
{
    public string Name { get; set; } = null!;

    public virtual ICollection<AccessoryHistory> AccessoryHistories { get; set; } = new List<AccessoryHistory>();

    public virtual ICollection<BatteryHistory> BatteryHistories { get; set; } = new List<BatteryHistory>();

    public virtual ICollection<PhoneHistory> PhoneHistories { get; set; } = new List<PhoneHistory>();

    public virtual ICollection<PhoneRepairHistory> PhoneRepairHistories { get; set; } = new List<PhoneRepairHistory>();

    public virtual ICollection<ScreenHistory> ScreenHistories { get; set; } = new List<ScreenHistory>();
}
