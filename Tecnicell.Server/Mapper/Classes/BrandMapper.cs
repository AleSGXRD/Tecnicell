using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class BrandMapper : IMapper<Brand, BrandViewModel>
    {
        public Brand ToModel(BrandViewModel viewmodel)
        {
            return new Brand
            {
                Name = viewmodel.Name,
                Description = viewmodel.Description,
            };
        }

        public BrandViewModel ToViewModel(Brand model)
        {
            return new BrandViewModel
            {
                Name = model.Name,
                Description = model.Description,
            };
        }
    }
}
