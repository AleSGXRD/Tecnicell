using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Mapper.Classes.BatteryMappers
{
    public class BatteryBrandMapper : IMapper<BatteryBrand, BatteryBrandViewModel>
    {
        public BatteryBrand ToModel(BatteryBrandViewModel viewmodel)
        {
            return new BatteryBrand
            {
                Name = viewmodel.Name,
                Description = viewmodel.Description,
            };
        }

        public BatteryBrandViewModel ToViewModel(BatteryBrand model)
        {
            return new BatteryBrandViewModel
            {
                Name = model.Name,
                Description = model.Description,
            };
        }
    }
}
