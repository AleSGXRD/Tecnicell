using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Mapper.Classes.Phone
{
    public class PhoneBrandMapper : IMapper<PhoneBrand, PhoneBrandViewModel>
    {
        public PhoneBrand ToModel(PhoneBrandViewModel viewmodel)
        {
            return new PhoneBrand{
                Name = viewmodel.Name,
                Description = viewmodel.Description,
            };
        }

        public PhoneBrandViewModel ToViewModel(PhoneBrand model)
        {
            return new PhoneBrandViewModel
            {
                Name = model.Name,
                Description = model.Description,
            };
        }
    }
}
