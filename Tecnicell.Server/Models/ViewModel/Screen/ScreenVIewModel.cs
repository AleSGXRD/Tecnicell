using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Models.ViewModel.Screen
{
    public class ScreenViewModel
    {
        public string ScreenCode { get; set; } = null!;

        public string? Brand { get; set; }

        public string? Name { get; set; }

        public int? Quantity { get; set; }

        public decimal? Width { get; set; }

        public decimal? Height { get; set; }

        public decimal? SalePrice { get; set; }

        public int? Warranty { get; set; }

        public virtual PhoneBrandViewModel? BrandNavigation { get; set; }

        public virtual ICollection<ScreenHistoryViewModel> ScreenHistories { get; set; } = new List<ScreenHistoryViewModel>();
    }
}
