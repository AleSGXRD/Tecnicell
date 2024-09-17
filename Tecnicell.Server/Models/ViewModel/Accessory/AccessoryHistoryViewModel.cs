using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel.Accessory
{
    public class AccessoryHistoryViewModel
    {
        public string AccessoryCode { get; set; } = null!;

        public DateTime Date { get; set; }

        public string? ActionHistory { get; set; }

        public string? ToBranch { get; set; }

        public string? Description { get; set; }

        public int? Quantity { get; set; }

        public string? SaleCode { get; set; }

        public virtual SaleViewModel? SaleCodeNavigation { get; set; }

        public virtual BranchViewModel? ToBranchNavigation { get; set; }
    }
}
