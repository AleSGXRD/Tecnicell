using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class ActionHistoryMapper : IMapper<ActionHistory, ActionHistoryViewModel>
    {
        public ActionHistory ToModel(ActionHistoryViewModel viewmodel)
        {
            return new ActionHistory
            {
                Name = viewmodel.Name,
            };
        }

        public ActionHistoryViewModel ToViewModel(ActionHistory model)
        {
            return new ActionHistoryViewModel { Name = model.Name, };
        }
    }
}
