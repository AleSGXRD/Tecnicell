using Tecnicell.Server.Mapper.Classes.Phone;
using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Mapper
{
    public class PhoneMapper : IMapper<Phone, PhoneViewModel>
    {
        public Phone ToModel(PhoneViewModel viewmodel)
        {
            PhoneBrandMapper phoneBrandMapper = new PhoneBrandMapper();
            PhoneHistoryMapper phoneHistoryMapper = new PhoneHistoryMapper();
            Phone model = new Phone
            {
                Imei = viewmodel.Imei,
                Brand = viewmodel.Brand,
                SalePrice = viewmodel.SalePrice,
            };
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = phoneBrandMapper.ToModel(viewmodel.BrandNavigation!);
            if (viewmodel.PhoneHistories != null) model.PhoneHistories = viewmodel.PhoneHistories.Select(history => phoneHistoryMapper.ToModel(history)).ToList();

            return model;
        }

        public PhoneViewModel ToViewModel(Phone model)
        {
            PhoneBrandMapper phoneBrandMapper = new PhoneBrandMapper();
            PhoneHistoryMapper phoneHistoryMapper = new PhoneHistoryMapper();
            PhoneViewModel viewmodel = new PhoneViewModel
            {
                Imei = model.Imei,
                Brand = model.Brand,
                SalePrice = model.SalePrice
            };
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = phoneBrandMapper.ToViewModel(model.BrandNavigation);
            if (model.PhoneHistories != null) viewmodel.PhoneHistories = model.PhoneHistories.Select(history => phoneHistoryMapper.ToViewModel(history)).ToList();

            return viewmodel;
        }
    }
}
