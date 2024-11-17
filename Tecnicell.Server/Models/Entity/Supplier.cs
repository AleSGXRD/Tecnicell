using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.Entity
{
    public class Supplier
    {
        public string SupplierCode { get; set; } = null!;   
        public string? Name { get; set; }

        public ICollection<AccessoryHistory>? AccessoryHistories { get; set; }
        public ICollection<BatteryHistory>? BatteryHistories { get; set; }
        public ICollection<PhoneHistory>? PhoneHistories { get; set; }
        public ICollection<ScreenHistory>? ScreenHistories { get; set; }
    }
}
