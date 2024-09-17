using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.Phone;

namespace Tecnicell.Server.Mapper.Classes.Phone
{
    public class PhoneRepairHistoryMapper : IMapper<PhoneRepairHistory, PhoneRepairHistoryViewModel>
    {
        public PhoneRepairHistory ToModel(PhoneRepairHistoryViewModel viewmodel)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            PhoneRepairHistory model = new PhoneRepairHistory { 
                Imei = viewmodel.Imei,
                Date = viewmodel.Date,
                Description = viewmodel.Description,
                SaleCode = viewmodel.SaleCode,
                ToBranch = viewmodel.ToBranch,
                ActionHistory = viewmodel.ActionHistory,
            };
            if (viewmodel.SaleCodeNavigation != null) model.SaleCodeNavigation = saleMapper.ToModel(viewmodel.SaleCodeNavigation);
            if (viewmodel.ToBranchNavigation != null) model.ToBranchNavigation = branchMapper.ToModel(viewmodel.ToBranchNavigation);

            return model;
        }

        public PhoneRepairHistoryViewModel ToViewModel(PhoneRepairHistory model)
        {
            SaleMapper saleMapper = new SaleMapper();
            BranchMapper branchMapper = new BranchMapper();
            PhoneRepairHistoryViewModel viewmodel = new PhoneRepairHistoryViewModel
            {
                Imei = model.Imei,
                Date = model.Date,
                Description = model.Description,
                SaleCode = model.SaleCode,
                ToBranch = model.ToBranch,
                ActionHistory = model.ActionHistory,
            };
            if (model.SaleCodeNavigation != null) viewmodel.SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation);
            if (model.ToBranchNavigation != null) viewmodel.ToBranchNavigation = branchMapper.ToViewModel(model.ToBranchNavigation);

            return viewmodel;
        }
    }
}
