using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class BranchMapper : IMapper<Branch, BranchViewModel>
    {
        public Branch ToModel(BranchViewModel viewmodel)
        {
            return new Branch
            {
                Name = viewmodel.Name,
                BranchCode = viewmodel.BranchCode,
            };
        }

        public BranchViewModel ToViewModel(Branch model)
        {
            return new BranchViewModel
            {
                Name = model.Name,
                BranchCode = model.BranchCode,
            };
        }
    }
}
