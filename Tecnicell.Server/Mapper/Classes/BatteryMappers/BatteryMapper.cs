using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Mapper.Classes.BatteryMappers
{
    public class BatteryMapper : IMapper<Battery, BatteryViewModel>
    {
        public Battery ToModel(BatteryViewModel viewmodel)
        {
            BatteryHistoryMapper batteryHistoryMapper = new BatteryHistoryMapper();
            BrandMapper brandMapper = new BrandMapper();
            ImageMapper imageMapper = new ImageMapper();
            Battery model = new Battery
            {
                BatteryCode = viewmodel.BatteryCode,
                Brand = viewmodel.Brand,
                Name = viewmodel.Name,
                SalePrice = viewmodel.SalePrice,
                Warranty = viewmodel.Warranty,
            };
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = brandMapper.ToModel(viewmodel.BrandNavigation);
            if (viewmodel.BatteryHistories != null) model.BatteryHistories = viewmodel.BatteryHistories.Select(battery => batteryHistoryMapper.ToModel(battery)).ToList();
            if (viewmodel.ImageCodeNavigation != null) model.ImageCodeNavigation = imageMapper.ToModel(viewmodel.ImageCodeNavigation);
            return model;
        }

        public BatteryViewModel ToViewModel(Battery model)
        {
            BatteryHistoryMapper batteryHistoryMapper = new BatteryHistoryMapper();
            BrandMapper brandMapper = new BrandMapper();
            ImageMapper imageMapper = new ImageMapper();
            BatteryViewModel viewmodel = new BatteryViewModel
            {
                BatteryCode = model.BatteryCode,
                Brand = model.Brand,
                Name = model.Name,
                SalePrice = model.SalePrice,
                Warranty = model.Warranty,
            };
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = brandMapper.ToViewModel(model.BrandNavigation);
            if (model.BatteryHistories != null) viewmodel.BatteryHistories = model.BatteryHistories.Select(battery => batteryHistoryMapper.ToViewModel(battery)).ToList();
            if (model.ImageCodeNavigation != null) viewmodel.ImageCodeNavigation = imageMapper.ToViewModel(model.ImageCodeNavigation);
            return viewmodel;
        }
    }
}
