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
            ImageMapper imageMapper = new ImageMapper();
            Accessory model = new Accessory
            {
                AccessoryCode = viewmodel.AccessoryCode,
                Name = viewmodel.Name,
                SalePrice = viewmodel.SalePrice,
                AccessoryType = viewmodel.AccessoryType
            };
            if (viewmodel.AccessoryHistories.Count > 0) model.AccessoryHistories = viewmodel.AccessoryHistories.Select(history => historyMapper.ToModel(history)).ToList();
            if (viewmodel.AccessoryTypeNavigation != null) model.AccessoryTypeNavigation = typeMapper.ToModel(viewmodel.AccessoryTypeNavigation);
            if (viewmodel.ImageCodeNavigation != null) model.ImageCodeNavigation = imageMapper.ToModel(viewmodel.ImageCodeNavigation);
            return model;
        }

        public AccessoryViewModel ToViewModel(Accessory model)
        {
            AccessoryTypeMapper typeMapper = new AccessoryTypeMapper();
            AccessoryHistoryMapper historyMapper = new AccessoryHistoryMapper();
            ImageMapper imageMapper = new ImageMapper();
            AccessoryViewModel viewmodel = new AccessoryViewModel
            {
                AccessoryCode = model.AccessoryCode,
                Name = model.Name,
                SalePrice = model.SalePrice,
                AccessoryType = model.AccessoryType
            };
            if(model.AccessoryHistories.Count > 0) viewmodel.AccessoryHistories = model.AccessoryHistories.Select(history => historyMapper.ToViewModel(history)).ToList();
            if(model.AccessoryTypeNavigation != null) viewmodel.AccessoryTypeNavigation = typeMapper.ToViewModel(model.AccessoryTypeNavigation);
            if (model.ImageCodeNavigation != null) viewmodel.ImageCodeNavigation = imageMapper.ToViewModel(model.ImageCodeNavigation);
            return viewmodel;
        }
    }
}
