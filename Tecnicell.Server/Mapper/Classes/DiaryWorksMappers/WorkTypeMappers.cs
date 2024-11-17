using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel.DiaryWork;

namespace Tecnicell.Server.Mapper.Classes.WorkDiaryMappers
{
    public class WorkTypeMappers : IMapper<WorkType, WorkTypeViewModel>
    {
        public WorkType ToModel(WorkTypeViewModel viewmodel)
        {
            return new WorkType
            {
                Name = viewmodel.Name
            };
        }

        public WorkTypeViewModel ToViewModel(WorkType model)
        {
            return new WorkTypeViewModel
            {
                Name = model.Name,
            };
        }
    }
}
