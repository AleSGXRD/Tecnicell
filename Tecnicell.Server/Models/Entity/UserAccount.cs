using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class UserAccount
{
    public string UserCode { get; set; } = null!;

    public string? Role { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
