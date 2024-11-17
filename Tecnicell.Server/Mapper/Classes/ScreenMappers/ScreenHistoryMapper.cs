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
            UserInfoMapper userInfoMapper = new UserInfoMapper();
            SupplierMapper supplierMapper = new SupplierMapper();

            ScreenHistory model = new ScreenHistory
            {
                ActionHistory = viewmodel.ActionHistory,
                UserCode = viewmodel.UserCode,
                Date = viewmodel.Date,
                Description = viewmodel.Description,
                Quantity = viewmodel.Quantity,
                SaleCode = viewmodel.SaleCode,
                ScreenCode = viewmodel.ScreenCode,
                ToBranch = viewmodel.ToBranch,
                SupplierCode = viewmodel.SupplierCode
            };
            if (viewmodel.SaleCodeNavigation != null) model.SaleCodeNavigation = saleMapper.ToModel(viewmodel.SaleCodeNavigation);
            if (viewmodel.ToBranchNavigation != null) model.ToBranchNavigation = branchMapper.ToModel(viewmodel.ToBranchNavigation);
            if (viewmodel.UserCodeNavigation != null) model.UserCodeNavigation = userInfoMapper.ToModel(viewmodel.UserCodeNavigation);
            if (viewmodel.SupplierNavigation != null) model.SupplierNavigation = supplierMapper.ToModel(viewmodel.SupplierNavigation);

            return model;
        }

        public ScreenHistoryViewModel ToViewModel(ScreenHistory model)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            UserInfoMapper userInfoMapper = new UserInfoMapper();
            SupplierMapper supplierMapper = new SupplierMapper();

            ScreenHistoryViewModel viewmodel = new ScreenHistoryViewModel
            {
                ActionHistory = model.ActionHistory,
                UserCode = model.UserCode,
                Date = model.Date,
                Description = model.Description,
                Quantity = model.Quantity,
                SaleCode = model.SaleCode,
                ScreenCode = model.ScreenCode,
                ToBranch = model.ToBranch,
                SupplierCode = model.SupplierCode
            };
            if (model.SaleCodeNavigation != null) viewmodel.SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation);
            if (model.ToBranchNavigation != null) viewmodel.ToBranchNavigation = branchMapper.ToViewModel(model.ToBranchNavigation);
            if (model.UserCodeNavigation != null) viewmodel.UserCodeNavigation = userInfoMapper.ToViewModel(model.UserCodeNavigation);
            if (model.SupplierNavigation != null) viewmodel.SupplierNavigation = supplierMapper.ToViewModel(model.SupplierNavigation);

            return viewmodel;
        }
    }
}
