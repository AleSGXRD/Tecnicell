using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.DiaryWork;

namespace Tecnicell.Server.Mapper.Classes.WorkDiaryMappers
{
    public class DiaryWorksMappers : IMapper<DiaryWork, DiaryWorkViewModel>
    {
        public DiaryWork ToModel(DiaryWorkViewModel viewmodel)
        {
            SaleMapper saleMapper = new SaleMapper();
            WorkTypeMappers workTypeMappers = new WorkTypeMappers();
            UserInfoMapper userInfoMapper = new UserInfoMapper();

            DiaryWork model =  new DiaryWork
            {
                Date = viewmodel.Date,
                Description = viewmodel.Description,
                SaleCode = viewmodel.SaleCode,
                WorkType = viewmodel.WorkType,
                UserCode = viewmodel.UserCode
            };
            if (viewmodel.SaleCodeNavigation != null) model.SaleCodeNavigation = saleMapper.ToModel(viewmodel.SaleCodeNavigation);
            if (viewmodel.WorkTypeNavigation != null) model.WorkTypeNavigation = workTypeMappers.ToModel(viewmodel.WorkTypeNavigation);
            if (viewmodel.UserCodeNavigation != null) model.UserCodeNavigation = userInfoMapper.ToModel(viewmodel.UserCodeNavigation);
            
            return model;
        }

        public DiaryWorkViewModel ToViewModel(DiaryWork model)
        {
            SaleMapper saleMapper = new SaleMapper();
            WorkTypeMappers workTypeMappers = new WorkTypeMappers();
            UserInfoMapper userInfoMapper = new UserInfoMapper();

            DiaryWorkViewModel viewmodel = new DiaryWorkViewModel
            {
                WorkType = model.WorkType,
                Date = model.Date,
                Description = model.Description,
                SaleCode = model.SaleCode,
                UserCode = model.UserCode
            };
            if (model.SaleCodeNavigation != null) viewmodel.SaleCodeNavigation = saleMapper.ToViewModel(model.SaleCodeNavigation);
            if (model.WorkTypeNavigation != null) viewmodel.WorkTypeNavigation = workTypeMappers.ToViewModel(model.WorkTypeNavigation);
            if (model.UserCodeNavigation != null) viewmodel.UserCodeNavigation = userInfoMapper.ToViewModel(model.UserCodeNavigation);
            
            return viewmodel;
        }
    }
}
