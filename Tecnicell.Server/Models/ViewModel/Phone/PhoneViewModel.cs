using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel.Phone
{
    public class PhoneViewModel
    {
        public string Imei { get; set; } = null!;

        public string? Brand { get; set; }

        public string? Name { get; set; }

        public decimal? SalePrice { get; set; }

        public virtual BrandViewModel? BrandNavigation { get; set; }

        public virtual ICollection<PhoneHistoryViewModel> PhoneHistories { get; set; } = new List<PhoneHistoryViewModel>();

        public virtual ImageViewModel? ImageCodeNavigation { get; set; }
    }
}
