using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Mapper.Classes.Phone
{
    public class PhoneHistoryMapper : IMapper<PhoneHistory, PhoneHistoryViewModel>
    {
        public PhoneHistory ToModel(PhoneHistoryViewModel viewmodel)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            UserInfoMapper userInfoMapper = new UserInfoMapper();
            PhoneHistory model = new PhoneHistory
            {
                Imei = viewmodel.Imei,
                UserCode = viewmodel.UserCode,
                Description = viewmodel.Description,
                ActionHistory = viewmodel.ActionHistory,
                Date = viewmodel.Date,
                SaleCode = viewmodel.SaleCode,
                ToBranch = viewmodel.ToBranch
            };
            if (viewmodel.SaleCodeNavigation != null) model.SaleCodeNavigation = saleMapper.ToModel(viewmodel.SaleCodeNavigation);
            if (viewmodel.ToBranchNavigation != null) model.ToBranchNavigation = branchMapper.ToModel(viewmodel.ToBranchNavigation);
            if (viewmodel.UserCodeNavigation != null) model.UserCodeNavigation = userInfoMapper.ToModel(viewmodel.UserCodeNavigation);

            return model;
        }

        public PhoneHistoryViewModel ToViewModel(PhoneHistory model)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            UserInfoMapper userInfoMapper = new UserInfoMapper();
            PhoneHistoryViewModel viewmodel = new PhoneHistoryViewModel
            {
                Imei = model.Imei,
                UserCode = model.UserCode,
                Description = model.Description,
                ActionHistory = model.ActionHistory,
                Date = model.Date,
                SaleCode = model.SaleCode,
                ToBranch = model.ToBranch,
            };
            if (model.SaleCodeNavigation != null) viewmodel.SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation);
            if (model.ToBranchNavigation != null) viewmodel.ToBranchNavigation = branchMapper.ToViewModel(model.ToBranchNavigation);
            if (model.UserCodeNavigation != null) viewmodel.UserCodeNavigation = userInfoMapper.ToViewModel(model.UserCodeNavigation);

            return viewmodel;
        }
    }
}
