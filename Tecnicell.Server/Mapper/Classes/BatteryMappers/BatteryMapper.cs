using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Mapper.Classes.BatteryMappers
{
    public class BatteryMapper : IMapper<Battery, BatteryViewModel>
    {
        public Battery ToModel(BatteryViewModel viewmodel)
        {
            BatteryHistoryMapper batteryHistoryMapper = new BatteryHistoryMapper();
            BatteryBrandMapper batteryBrandMapper = new BatteryBrandMapper();
            Battery model = new Battery
            {
                BatteryCode = viewmodel.BatteryCode,
                Brand = viewmodel.Brand,
                Name = viewmodel.Name,
                Quantity = viewmodel.Quantity,
                SalePrice = viewmodel.SalePrice,
                Warranty = viewmodel.Warranty,
            };
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = batteryBrandMapper.ToModel(viewmodel.BrandNavigation!);
            if (viewmodel.BatteryHistories != null) model.BatteryHistories = viewmodel.BatteryHistories.Select(battery => batteryHistoryMapper.ToModel(battery)).ToList();
        
            return model;
        }

        public BatteryViewModel ToViewModel(Battery model)
        {
            BatteryHistoryMapper batteryHistoryMapper = new BatteryHistoryMapper();
            BatteryBrandMapper batteryBrandMapper = new BatteryBrandMapper();
            BatteryViewModel viewmodel = new BatteryViewModel
            {
                BatteryCode = model.BatteryCode,
                Brand = model.Brand,
                Name = model.Name,
                Quantity = model.Quantity,
                SalePrice = model.SalePrice,
                Warranty = model.Warranty,
            };
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = batteryBrandMapper.ToViewModel(model.BrandNavigation!);
            if (model.BatteryHistories != null) viewmodel.BatteryHistories = model.BatteryHistories.Select(battery => batteryHistoryMapper.ToViewModel(battery)).ToList();
        
            return viewmodel;
        }
    }
}
