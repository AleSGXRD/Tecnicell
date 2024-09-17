using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class UserAccountMapper : IMapper<UserAccount, UserAccountViewModel>
    {
        public UserAccount ToModel(UserAccountViewModel viewmodel)
        {
            RoleMapper roleMapper = new RoleMapper();
            UserAccount model = new UserAccount
            {
                UserCode = viewmodel.UserCode,
                Role = viewmodel.Role,
                Name = viewmodel.Name,
                Password = viewmodel.Password,
            };
            if (viewmodel.RoleNavigation != null) model.RoleNavigation = roleMapper.ToModel(viewmodel.RoleNavigation!);

            return model;
        }

        public UserAccountViewModel ToViewModel(UserAccount model)
        {
            RoleMapper roleMapper = new RoleMapper();
            UserAccountViewModel viewmodel = new UserAccountViewModel
            {
                UserCode = model.UserCode,
                Role = model.Role,
                Name = model.Name,
                Password = model.Password,
            };
            if (model.RoleNavigation != null) viewmodel.RoleNavigation = roleMapper.ToViewModel(model.RoleNavigation!);

            return viewmodel;
        }
    }
}
