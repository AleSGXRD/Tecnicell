using Microsoft.Build.Framework;

namespace Tecnicell.Server.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
