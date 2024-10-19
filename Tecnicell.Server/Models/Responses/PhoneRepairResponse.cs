using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Models.Responses
{
    public class PhoneRepairResponse
    {
        public PhoneRepairView? View { get; set; }
        public ImageViewModel? Image { get; set; }
        public IEnumerable<PhoneRepairHistoryViewModel> Histories { get; set; } = new List<PhoneRepairHistoryViewModel>();
    }
}
