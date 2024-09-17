using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Screen;

namespace Tecnicell.Server.Mapper.Classes.ScreenMappers
{
    public class ScreenHistoryMapper : IMapper<ScreenHistory, ScreenHistoryViewModel>
    {
        public ScreenHistory ToModel(ScreenHistoryViewModel viewmodel)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            ScreenHistory model = new ScreenHistory
            {
                ActionHistory = viewmodel.ActionHistory,
                Date = viewmodel.Date,
                Description = viewmodel.Description,
                Quantity = viewmodel.Quantity,
                SaleCode = viewmodel.SaleCode,
                ScreenCode = viewmodel.ScreenCode,
                ToBranch = viewmodel.ToBranch,
            };
            if (viewmodel.SaleCodeNavigation != null) model.SaleCodeNavigation = saleMapper.ToModel(viewmodel.SaleCodeNavigation);
            if (viewmodel.ToBranchNavigation != null) model.ToBranchNavigation = branchMapper.ToModel(viewmodel.ToBranchNavigation);

            return model;
        }

        public ScreenHistoryViewModel ToViewModel(ScreenHistory model)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            ActionHistoryMapper actionHistoryMapper = new ActionHistoryMapper();
            ScreenHistoryViewModel viewmodel = new ScreenHistoryViewModel
            {
                ActionHistory = model.ActionHistory,
                Date = model.Date,
                Description = model.Description,
                Quantity = model.Quantity,
                SaleCode = model.SaleCode,
                ScreenCode = model.ScreenCode,
                ToBranch = model.ToBranch,
            };
            if (model.SaleCodeNavigation != null) viewmodel.SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation);
            if (model.ToBranchNavigation != null) viewmodel.ToBranchNavigation = branchMapper.ToViewModel(model.ToBranchNavigation);

            return viewmodel;
        }
    }
}
