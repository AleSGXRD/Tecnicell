namespace Tecnicell.Server.Models.ViewModel.Accessory
{
    public class AccessoryViewModel
    {
        public string AccessoryCode { get; set; } = null!;

        public string? Name { get; set; }

        public string? AccessoryType { get; set; }

        public decimal? SalePrice { get; set; }

        public int? Quantity { get; set; }

        public virtual ICollection<AccessoryHistoryViewModel> AccessoryHistories { get; set; } = new List<AccessoryHistoryViewModel>();

        public virtual AccessoryTypeViewModel? AccessoryTypeNavigation { get; set; }
    }
}
