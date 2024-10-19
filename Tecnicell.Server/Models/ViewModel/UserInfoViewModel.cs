using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel
{
    public class UserInfoViewModel
    {
        public string UserCode { get; set; } = null!;

        public string? Role { get; set; }

        public string? Branch { get; set; }

        public string? ImageCode { get; set; }

        public string? Name { get; set; }

        public virtual BranchViewModel? BranchNavigation { get; set; }

        public virtual ImageViewModel? ImageCodeNavigation { get; set; }

        public virtual RoleViewModel? RoleNavigation { get; set; }
    }
}
