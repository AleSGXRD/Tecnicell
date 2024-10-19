using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class UserInfoMapper : IMapper<UserInfo, UserInfoViewModel>
    {
        public UserInfo ToModel(UserInfoViewModel viewmodel)
        {
            BranchMapper branchMapper = new BranchMapper();
            ImageMapper imageMapper = new ImageMapper();
            RoleMapper roleMapper = new RoleMapper();

            UserInfo model = new UserInfo
            {
                UserCode = viewmodel.UserCode,
                Name = viewmodel.Name,
                Branch = viewmodel.Branch,
                Role = viewmodel.Role,
                ImageCode = viewmodel.ImageCode,
            };
            if (viewmodel.BranchNavigation != null) { model.BranchNavigation = branchMapper.ToModel(viewmodel.BranchNavigation); }
            if (viewmodel.ImageCodeNavigation != null) { model.ImageCodeNavigation = imageMapper.ToModel(viewmodel.ImageCodeNavigation); }
            if (viewmodel.RoleNavigation != null) { model.RoleNavigation = roleMapper.ToModel(viewmodel.RoleNavigation); }

            return model;
        }

        public UserInfoViewModel ToViewModel(UserInfo model)
        {
            BranchMapper branchMapper = new BranchMapper();
            ImageMapper imageMapper = new ImageMapper();
            RoleMapper roleMapper = new RoleMapper();

            UserInfoViewModel viewmodel = new UserInfoViewModel { 
                UserCode = model.UserCode,
                Name = model.Name,
                ImageCode = model.ImageCode,
                Branch = model.Branch, 
                Role = model.Role
            };
            if (model.BranchNavigation != null) { viewmodel.BranchNavigation = branchMapper.ToViewModel(model.BranchNavigation); }
            if (model.ImageCodeNavigation != null) { viewmodel.ImageCodeNavigation = imageMapper.ToViewModel(model.ImageCodeNavigation); }
            if (model.RoleNavigation != null) { viewmodel.RoleNavigation = roleMapper.ToViewModel(model.RoleNavigation); }

            return viewmodel;
        }
    }
}
