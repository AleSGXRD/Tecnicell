using System;
using System.Collections.Generic;

namespace Tecnicell.Server.Models.Entity;

public partial class Image
{
    public string Imagecode { get; set; } = null!;

    public string? Name { get; set; }

    public byte[]? File { get; set; }
}
