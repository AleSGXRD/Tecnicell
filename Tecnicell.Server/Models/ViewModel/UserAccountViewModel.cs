using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel
{
    public class UserAccountViewModel
    {
        public string UserCode { get; set; } = null!;

        public string? Role { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public virtual RoleViewModel? RoleNavigation { get; set; }
    }
}
