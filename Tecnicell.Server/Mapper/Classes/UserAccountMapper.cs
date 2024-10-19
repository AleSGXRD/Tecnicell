using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class UserAccountMapper : IMapper<UserAccount, UserAccountViewModel>
    {
        public UserAccount ToModel(UserAccountViewModel viewmodel)
        {
            RoleMapper roleMapper = new RoleMapper();
            ImageMapper imageMapper = new ImageMapper();
            UserAccount model = new UserAccount
            {
                UserCode = viewmodel.UserCode,
                Name = viewmodel.Name,
                Password = viewmodel.Password,
            };

            return model;
        }

        public UserAccountViewModel ToViewModel(UserAccount model)
        {
            RoleMapper roleMapper = new RoleMapper();
            ImageMapper imageMapper = new ImageMapper();
            UserAccountViewModel viewmodel = new UserAccountViewModel
            {
                UserCode = model.UserCode,
                Name = model.Name,
                Password = model.Password,
            };
            return viewmodel;
        }
    }
}
