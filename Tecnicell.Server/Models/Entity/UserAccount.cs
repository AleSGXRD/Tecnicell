using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class UserAccount
{
    public string? Name { get; set; }

    public string? Password { get; set; }

    public string UserCode { get; set; } = null!;

    public virtual UserInfo UserCodeNavigation { get; set; } = null!;
}
