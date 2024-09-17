using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class RoleMapper : IMapper<Role, RoleViewModel>
    {
        public Role ToModel(RoleViewModel viewmodel)
        {
            return new Role
            {
                Name = viewmodel.Name,
                RoleCode = viewmodel.RoleCode,
            };
        }

        public RoleViewModel ToViewModel(Role model)
        {
            return new RoleViewModel
            { 
                Name = model.Name, 
                RoleCode = model.RoleCode
            };
        }
    }
}
