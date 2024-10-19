using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel
{
    public class UserAccountViewModel
    {
        public string UserCode { get; set; } = null!;

        public string? Name { get; set; }

        public string? Password { get; set; }
    }
}
