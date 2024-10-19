using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Models.Responses
{
    public class PhoneResponse
    {
        public PhoneView? View { get; set; }
        public ImageViewModel? Image { get; set; }
        public IEnumerable<PhoneHistoryViewModel> Histories { get; set; } = new List<PhoneHistoryViewModel>();
    }
}
