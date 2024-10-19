using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Image
{
    public string ImageCode { get; set; } = null!;

    public string? Name { get; set; }

    public byte[]? File { get; set; }

    public virtual ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();

    public virtual ICollection<Battery> Batteries { get; set; } = new List<Battery>();

    public virtual ICollection<PhoneRepair> PhoneRepairs { get; set; } = new List<PhoneRepair>();

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    public virtual ICollection<Screen> Screens { get; set; } = new List<Screen>();

    public virtual ICollection<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
}
