using Tecnicell.Server.Mapper.Classes;
using Tecnicell.Server.Mapper.Classes.Phone;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Mapper
{
    public class PhoneMapper : IMapper<Phone, PhoneViewModel>
    {
        public Phone ToModel(PhoneViewModel viewmodel)
        {
            BrandMapper brandMapper = new BrandMapper();
            PhoneHistoryMapper phoneHistoryMapper = new PhoneHistoryMapper();
            ImageMapper imageMapper = new ImageMapper();
            Phone model = new Phone
            {
                Imei = viewmodel.Imei,
                Brand = viewmodel.Brand,
                Name = viewmodel.Name,  
                SalePrice = viewmodel.SalePrice,
            };
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = brandMapper.ToModel(viewmodel.BrandNavigation);
            if (viewmodel.PhoneHistories != null) model.PhoneHistories = viewmodel.PhoneHistories.Select(history => phoneHistoryMapper.ToModel(history)).ToList();
            if (viewmodel.ImageCodeNavigation != null) model.ImageCodeNavigation = imageMapper.ToModel(viewmodel.ImageCodeNavigation);
            return model;
        }

        public PhoneViewModel ToViewModel(Phone model)
        {
            BrandMapper brandMapper = new BrandMapper();
            PhoneHistoryMapper phoneHistoryMapper = new PhoneHistoryMapper();
            ImageMapper imageMapper = new ImageMapper();
            PhoneViewModel viewmodel = new PhoneViewModel
            {
                Imei = model.Imei,
                Brand = model.Brand,
                Name = model.Name,
                SalePrice = model.SalePrice
            };
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = brandMapper.ToViewModel(model.BrandNavigation);
            if (model.PhoneHistories != null) viewmodel.PhoneHistories = model.PhoneHistories.Select(history => phoneHistoryMapper.ToViewModel(history)).ToList();
            if (model.ImageCodeNavigation != null) viewmodel.ImageCodeNavigation = imageMapper.ToViewModel(model.ImageCodeNavigation);
            return viewmodel;
        }
    }
}
