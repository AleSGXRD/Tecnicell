using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Role
{
    public string RoleCode { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
}
