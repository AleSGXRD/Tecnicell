using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Accessory;

namespace Tecnicell.Server.Mapper.Classes.AccessoryMappers
{
    public class AccessoryMapper : IMapper<Accessory, AccessoryViewModel>
    {
        public Accessory ToModel(AccessoryViewModel viewmodel)
        {
            AccessoryTypeMapper typeMapper = new AccessoryTypeMapper();
            AccessoryHistoryMapper historyMapper = new AccessoryHistoryMapper();
            Accessory model = new Accessory
            {
                AccessoryCode = viewmodel.AccessoryCode,
                Name = viewmodel.Name,
                Quantity = viewmodel.Quantity,
                SalePrice = viewmodel.SalePrice,
                AccessoryType = viewmodel.AccessoryType
            };
            if (viewmodel.AccessoryHistories != null) model.AccessoryHistories = viewmodel.AccessoryHistories.Select(history => historyMapper.ToModel(history)).ToList();
            if (viewmodel.AccessoryTypeNavigation != null) model.AccessoryTypeNavigation = typeMapper.ToModel(viewmodel.AccessoryTypeNavigation);
            return model;
        }

        public AccessoryViewModel ToViewModel(Accessory model)
        {
            AccessoryTypeMapper typeMapper = new AccessoryTypeMapper();
            AccessoryHistoryMapper historyMapper = new AccessoryHistoryMapper();
            AccessoryViewModel viewmodel = new AccessoryViewModel
            {
                AccessoryCode = model.AccessoryCode,
                Name = model.Name,
                Quantity = model.Quantity,
                SalePrice = model.SalePrice,
                AccessoryType = model.AccessoryType
            };
            if(model.AccessoryHistories != null) viewmodel.AccessoryHistories = model.AccessoryHistories.Select(history => historyMapper.ToViewModel(history)).ToList();
            if(model.AccessoryTypeNavigation != null) viewmodel.AccessoryTypeNavigation = typeMapper.ToViewModel(model.AccessoryTypeNavigation);
            return viewmodel;
        }
    }
}
