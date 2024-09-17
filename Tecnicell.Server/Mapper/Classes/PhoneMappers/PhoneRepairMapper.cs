using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Mapper.Classes.Phone
{
    public class PhoneRepairMapper : IMapper<PhoneRepair, PhoneRepairViewModel>
    {
        public PhoneRepair ToModel(PhoneRepairViewModel viewmodel)
        {
            PhoneBrandMapper phoneBrandMapper = new PhoneBrandMapper();
            PhoneRepairHistoryMapper phoneRepairHistoryMapper = new PhoneRepairHistoryMapper();
            PhoneRepair model = new PhoneRepair
            {
                Imei = viewmodel.Imei,
                Brand = viewmodel.Brand,
                CustomerId = viewmodel.CustomerId,
                CustomerName = viewmodel.CustomerName,
                CustomerNumber = viewmodel.CustomerNumber,
                Price = viewmodel.Price
            };
            if (viewmodel.PhoneRepairHistories != null) model.PhoneRepairHistories = viewmodel.PhoneRepairHistories.Select(history => phoneRepairHistoryMapper.ToModel(history)).ToList();
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = phoneBrandMapper.ToModel(viewmodel.BrandNavigation);

            return model;
        }

        public PhoneRepairViewModel ToViewModel(PhoneRepair model)
        {
            PhoneBrandMapper phoneBrandMapper = new PhoneBrandMapper();
            PhoneRepairHistoryMapper phoneRepairHistoryMapper = new PhoneRepairHistoryMapper();
            PhoneRepairViewModel viewmodel =  new PhoneRepairViewModel
            {
                Imei = model.Imei,
                Brand = model.Brand,
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                CustomerNumber = model.CustomerNumber,
                Price = model.Price
            };
            if (model.PhoneRepairHistories != null) viewmodel.PhoneRepairHistories = model.PhoneRepairHistories.Select(history => phoneRepairHistoryMapper.ToViewModel(history)).ToList();
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = phoneBrandMapper.ToViewModel(model.BrandNavigation);

            return viewmodel;

        }
    }
}
