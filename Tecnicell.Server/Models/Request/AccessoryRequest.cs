using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;
using Tecnicell.Server.Models.ViewModel;
using Tecnicell.Server.Models.ViewModel.Battery;
using Tecnicell.Server.Models.ViewModel.Accessory;

namespace Tecnicell.Server.Models.Request
{
    public class AccessoryRequest
    {
        public Accessory? Model { get; set; }
        public ImageViewModel? Image { get; set; }
        public AccessoryHistory? History { get; set; }
    }
}
