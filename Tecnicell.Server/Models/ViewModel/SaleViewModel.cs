using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel
{
    public class SaleViewModel
    {
        public string SaleCode { get; set; } = null!;

        public string? CurrencyCode { get; set; }

        public DateTime? Warranty { get; set; }

        public decimal? Cost { get; set; }

        public virtual CurrencyViewModel? CurrencyCodeNavigation { get; set; }
    }
}
