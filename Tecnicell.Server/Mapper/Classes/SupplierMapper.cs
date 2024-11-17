using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class SupplierMapper : IMapper<Supplier, SupplierViewModel>
    {
        public Supplier ToModel(SupplierViewModel viewmodel)
        {
            return new Supplier
            {
                Name = viewmodel.Name,
                SupplierCode = viewmodel.SupplierCode
            };
        }

        public SupplierViewModel ToViewModel(Supplier model)
        {
            return new SupplierViewModel
            {
                Name = model.Name,
                SupplierCode = model.SupplierCode,
            };
        }
    }
}
