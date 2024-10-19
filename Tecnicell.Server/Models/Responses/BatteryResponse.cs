using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;
using Tecnicell.Server.Models.ViewModel;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Models.Responses
{
    public class BatteryResponse
    {
        public BatteryView? View { get; set; }
        public ImageViewModel? Image { get; set; }
        public IEnumerable<BatteryHistoryViewModel> Histories { get; set; } = new List<BatteryHistoryViewModel>();
    }
}
