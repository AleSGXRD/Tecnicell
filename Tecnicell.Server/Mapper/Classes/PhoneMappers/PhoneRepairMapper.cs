using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Mapper.Classes.Phone
{
    public class PhoneRepairMapper : IMapper<PhoneRepair, PhoneRepairViewModel>
    {
        public PhoneRepair ToModel(PhoneRepairViewModel viewmodel)
        {
            PhoneRepairHistoryMapper phoneRepairHistoryMapper = new PhoneRepairHistoryMapper();
            BrandMapper brandMapper = new BrandMapper();
            ImageMapper imageMapper = new ImageMapper();
            PhoneRepair model = new PhoneRepair
            {
                Imei = viewmodel.Imei,
                Brand = viewmodel.Brand,
                Name = viewmodel.Name,
                CustomerId = viewmodel.CustomerId,
                CustomerName = viewmodel.CustomerName,
                CustomerNumber = viewmodel.CustomerNumber,
                Price = viewmodel.Price
            };
            if (viewmodel.PhoneRepairHistories != null) model.PhoneRepairHistories = viewmodel.PhoneRepairHistories.Select(history => phoneRepairHistoryMapper.ToModel(history)).ToList();
            if (viewmodel.BrandNavigation != null) model.BrandNavigation = brandMapper.ToModel(viewmodel.BrandNavigation);
            if (viewmodel.ImageCodeNavigation != null) model.ImageCodeNavigation = imageMapper.ToModel(viewmodel.ImageCodeNavigation);
            return model;
        }

        public PhoneRepairViewModel ToViewModel(PhoneRepair model)
        {
            PhoneRepairHistoryMapper phoneRepairHistoryMapper = new PhoneRepairHistoryMapper();
            BrandMapper brandMapper = new BrandMapper();
            ImageMapper imageMapper = new ImageMapper();
            PhoneRepairViewModel viewmodel =  new PhoneRepairViewModel
            {
                Imei = model.Imei,
                Brand = model.Brand,
                Name = model.Name,
                CustomerId = model.CustomerId,
                CustomerName = model.CustomerName,
                CustomerNumber = model.CustomerNumber,
                Price = model.Price
            };
            if (model.PhoneRepairHistories != null) viewmodel.PhoneRepairHistories = model.PhoneRepairHistories.Select(history => phoneRepairHistoryMapper.ToViewModel(history)).ToList();
            if (model.BrandNavigation != null) viewmodel.BrandNavigation = brandMapper.ToViewModel(model.BrandNavigation);
            if (model.ImageCodeNavigation != null) viewmodel.ImageCodeNavigation = imageMapper.ToViewModel(model.ImageCodeNavigation);
            return viewmodel;

        }
    }
}
