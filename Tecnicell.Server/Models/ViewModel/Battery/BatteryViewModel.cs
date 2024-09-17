using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel.Battery
{
    public class BatteryViewModel
    {
        public string BatteryCode { get; set; } = null!;

        public string? Name { get; set; }

        public string? Brand { get; set; }

        public int? Quantity { get; set; }

        public decimal? SalePrice { get; set; }

        public int? Warranty { get; set; }

        public virtual ICollection<BatteryHistoryViewModel> BatteryHistories { get; set; } = new List<BatteryHistoryViewModel>();

        public virtual BatteryBrandViewModel? BrandNavigation { get; set; }
    }
}
