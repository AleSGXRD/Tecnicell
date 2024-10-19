using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;
using Tecnicell.Server.Models.ViewModel;
using Tecnicell.Server.Models.ViewModel.Battery;
using Tecnicell.Server.Models.ViewModel.Accessory;
using Tecnicell.Server.Models.ViewModel.Screen;

namespace Tecnicell.Server.Models.Responses
{
    public class ScreenResponse
    {
        public ScreenView? View { get; set; }
        public ImageViewModel? Image { get; set; }
        public IEnumerable<ScreenHistoryViewModel> Histories { get; set; } = new List<ScreenHistoryViewModel>();
    }
}
