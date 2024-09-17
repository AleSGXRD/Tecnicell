using Tecnicell.Server.Mapper.Classes.Phone;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Screen;

namespace Tecnicell.Server.Mapper.Classes.ScreenMappers
{
    public class ScreenMapper : IMapper<Screen, ScreenViewModel>
    {
        public Screen ToModel(ScreenViewModel viewmodel)
        {
            PhoneBrandMapper phoneBrandMapper = new PhoneBrandMapper();
            ScreenHistoryMapper historyMapper = new ScreenHistoryMapper();
            Screen model = new Screen
            {
                ScreenCode = viewmodel.ScreenCode,
                Brand = viewmodel.Brand,
                Height = viewmodel.Height,
                Width = viewmodel.Width,
                Quantity = viewmodel.Quantity,
                Warranty = viewmodel.Warranty,
                SalePrice = viewmodel.SalePrice,
                Name = viewmodel.Name,
                BrandNavigation = phoneBrandMapper.ToModel(viewmodel.BrandNavigation!),
                ScreenHistories = viewmodel.ScreenHistories.Select(history => historyMapper.ToModel(history)).ToList(),
            };
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = phoneBrandMapper.ToModel(viewmodel.BrandNavigation!);
            if (viewmodel.ScreenHistories != null) model.ScreenHistories = viewmodel.ScreenHistories.Select(history => historyMapper.ToModel(history)).ToList();

            return model;
        }

        public ScreenViewModel ToViewModel(Screen model)
        {
            PhoneBrandMapper phoneBrandMapper = new PhoneBrandMapper();
            ScreenHistoryMapper historyMapper = new ScreenHistoryMapper();
            ScreenViewModel viewmodel = new ScreenViewModel
            {
                ScreenCode = model.ScreenCode,
                Brand = model.Brand,
                Height = model.Height,
                Width = model.Width,
                Quantity = model.Quantity,
                Warranty = model.Warranty,
                SalePrice = model.SalePrice,
                Name = model.Name,
                BrandNavigation = phoneBrandMapper.ToViewModel(model.BrandNavigation!),
                ScreenHistories = model.ScreenHistories.Select(history => historyMapper.ToViewModel(history)).ToList(),
            };
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = phoneBrandMapper.ToViewModel(model.BrandNavigation!);
            if (model.ScreenHistories != null) viewmodel.ScreenHistories = model.ScreenHistories.Select(history => historyMapper.ToViewModel(history)).ToList();

            return viewmodel;
        }
    }
}
