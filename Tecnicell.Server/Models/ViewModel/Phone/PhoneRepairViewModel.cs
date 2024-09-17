using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel.Phone
{
    public class PhoneRepairViewModel
    {
        public string Imei { get; set; } = null!;

        public string? Brand { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerId { get; set; }

        public string? CustomerNumber { get; set; }

        public decimal? Price { get; set; }

        public virtual PhoneBrandViewModel? BrandNavigation { get; set; }

        public virtual ICollection<PhoneRepairHistoryViewModel> PhoneRepairHistories { get; set; } = new List<PhoneRepairHistoryViewModel>();
    }
}
