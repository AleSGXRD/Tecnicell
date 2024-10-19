using Tecnicell.Server.Mapper.Classes.Phone;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Screen;

namespace Tecnicell.Server.Mapper.Classes.ScreenMappers
{
    public class ScreenMapper : IMapper<Screen, ScreenViewModel>
    {
        public Screen ToModel(ScreenViewModel viewmodel)
        {
            BrandMapper brandMapper = new BrandMapper();
            ScreenHistoryMapper historyMapper = new ScreenHistoryMapper();
            ImageMapper imageMapper = new ImageMapper();
            Screen model = new Screen
            {
                ScreenCode = viewmodel.ScreenCode,
                Brand = viewmodel.Brand,
                Height = viewmodel.Height,
                Width = viewmodel.Width,
                Warranty = viewmodel.Warranty,
                SalePrice = viewmodel.SalePrice,
                Name = viewmodel.Name
            };
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = brandMapper.ToModel(viewmodel.BrandNavigation);
            if (viewmodel.ScreenHistories != null) model.ScreenHistories = viewmodel.ScreenHistories.Select(history => historyMapper.ToModel(history)).ToList();
            if (viewmodel.ImageCodeNavigation != null) model.ImageCodeNavigation = imageMapper.ToModel(viewmodel.ImageCodeNavigation);
            return model;
        }

        public ScreenViewModel ToViewModel(Screen model)
        {
            BrandMapper brandMapper = new BrandMapper();
            ScreenHistoryMapper historyMapper = new ScreenHistoryMapper();
            ImageMapper imageMapper = new ImageMapper();
            ScreenViewModel viewmodel = new ScreenViewModel
            {
                ScreenCode = model.ScreenCode,
                Brand = model.Brand,
                Height = model.Height,
                Width = model.Width,
                Warranty = model.Warranty,
                SalePrice = model.SalePrice,
                Name = model.Name
            };
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = brandMapper.ToViewModel(model.BrandNavigation);
            if (model.ScreenHistories != null) viewmodel.ScreenHistories = model.ScreenHistories.Select(history => historyMapper.ToViewModel(history)).ToList();
            if (model.ImageCodeNavigation != null) viewmodel.ImageCodeNavigation = imageMapper.ToViewModel(model.ImageCodeNavigation);
            return viewmodel;
        }
    }
}
