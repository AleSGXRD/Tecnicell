using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Accessory;

namespace Tecnicell.Server.Mapper.Classes.AccessoryMappers
{
    public class AccessoryTypeMapper : IMapper<AccessoryType, AccessoryTypeViewModel>
    {
        public AccessoryType ToModel(AccessoryTypeViewModel viewmodel)
        {
            return new AccessoryType
            {
                AccessoryTypeCode = viewmodel.AccessoryTypeCode,
                Name = viewmodel.Name,
            };
        }

        public AccessoryTypeViewModel ToViewModel(AccessoryType model)
        {
            return new AccessoryTypeViewModel
            {
                AccessoryTypeCode = model.AccessoryTypeCode,
                Name = model.Name,
            };
        }
    }
}
