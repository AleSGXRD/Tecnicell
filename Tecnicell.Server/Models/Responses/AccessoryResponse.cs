using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;
using Tecnicell.Server.Models.ViewModel;
using Tecnicell.Server.Models.ViewModel.Battery;
using Tecnicell.Server.Models.ViewModel.Accessory;

namespace Tecnicell.Server.Models.Responses
{
    public class AccessoryResponse
    {
        public AccessoryView? View { get; set; }
        public ImageViewModel? Image { get; set; }
        public IEnumerable<AccessoryHistoryViewModel> Histories { get; set; } = new List<AccessoryHistoryViewModel>();
    }
}
