using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Accessory;

namespace Tecnicell.Server.Mapper.Classes.AccessoryMappers
{
    public class AccessoryHistoryMapper : IMapper<AccessoryHistory, AccessoryHistoryViewModel>
    {
        public AccessoryHistory ToModel(AccessoryHistoryViewModel viewmodel)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            AccessoryHistory model = new AccessoryHistory
            {
                AccessoryCode = viewmodel.AccessoryCode,
                Date = viewmodel.Date,
                Description = viewmodel.Description,
                Quantity = viewmodel.Quantity,
                SaleCode = viewmodel.SaleCode,
                ToBranch = viewmodel.ToBranch,
                ActionHistory = viewmodel.ActionHistory,
            };
            if(viewmodel.SaleCodeNavigation != null) model.SaleCodeNavigation = saleMapper.ToModel(viewmodel.SaleCodeNavigation!);
            if(viewmodel.ToBranchNavigation != null) model.ToBranchNavigation = branchMapper.ToModel(viewmodel.ToBranchNavigation!);

            return model;
        }

        public AccessoryHistoryViewModel ToViewModel(AccessoryHistory model)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            AccessoryHistoryViewModel viewmodel = new AccessoryHistoryViewModel
            {
                AccessoryCode = model.AccessoryCode,
                Date = model.Date,
                Description = model.Description,
                Quantity = model.Quantity,
                SaleCode = model.SaleCode,
                ToBranch = model.ToBranch,
                ActionHistory = model.ActionHistory,
                SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation!),
                ToBranchNavigation = branchMapper.ToViewModel(model.ToBranchNavigation!),
            };
            if (model.SaleCodeNavigation != null) viewmodel.SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation!);
            if (model.ToBranchNavigation != null) viewmodel.ToBranchNavigation = branchMapper.ToViewModel(model.ToBranchNavigation!);

            return viewmodel;
        }
    }
}
