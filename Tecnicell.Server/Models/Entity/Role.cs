using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Role
{
    public string RoleCode { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
