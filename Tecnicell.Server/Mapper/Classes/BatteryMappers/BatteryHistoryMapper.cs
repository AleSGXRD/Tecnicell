using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Battery;

namespace Tecnicell.Server.Mapper.Classes.BatteryMappers
{
    public class BatteryHistoryMapper : IMapper<BatteryHistory, BatteryHistoryViewModel>
    {
        public BatteryHistory ToModel(BatteryHistoryViewModel viewmodel)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            UserInfoMapper userInfoMapper = new UserInfoMapper();

            BatteryHistory model = new BatteryHistory {
                BatteryCode = viewmodel.BatteryCode,
                UserCode = viewmodel.UserCode,
                Date = viewmodel.Date,
                Description = viewmodel.Description,
                Quantity = viewmodel.Quantity,
                SaleCode = viewmodel.SaleCode,
                ToBranch = viewmodel.ToBranch,
                ActionHistory = viewmodel.ActionHistory,
            };
            if (viewmodel.SaleCodeNavigation != null) model.SaleCodeNavigation = saleMapper.ToModel(viewmodel.SaleCodeNavigation);
            if (viewmodel.ToBranchNavigation != null) model.ToBranchNavigation = branchMapper.ToModel(viewmodel.ToBranchNavigation);
            if (viewmodel.UserCodeNavigation != null) model.UserCodeNavigation = userInfoMapper.ToModel(viewmodel.UserCodeNavigation);

            return model;
        }

        public BatteryHistoryViewModel ToViewModel(BatteryHistory model)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            UserInfoMapper userInfoMapper = new UserInfoMapper();

            BatteryHistoryViewModel viewmodel = new BatteryHistoryViewModel
            {
                BatteryCode = model.BatteryCode,
                UserCode = model.UserCode,
                Date = model.Date,
                Description = model.Description,
                Quantity = model.Quantity,
                SaleCode = model.SaleCode,
                ToBranch = model.ToBranch,
                ActionHistory = model.ActionHistory,
            };
            if (model.SaleCodeNavigation != null) viewmodel.SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation);
            if (model.ToBranchNavigation != null) viewmodel.ToBranchNavigation = branchMapper.ToViewModel(model.ToBranchNavigation);
            if (model.UserCodeNavigation != null) viewmodel.UserCodeNavigation = userInfoMapper.ToViewModel(model.UserCodeNavigation);

            return viewmodel;
        }
    }
}
