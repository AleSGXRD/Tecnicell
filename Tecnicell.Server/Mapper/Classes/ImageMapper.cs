using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class ImageMapper : IMapper<Image, ImageViewModel>
    {
        public Image ToModel(ImageViewModel viewmodel)
        {
            return new Image
            {
                Name = viewmodel.Name,
                Imagecode = viewmodel.Imagecode,
                File = viewmodel.File,
            };
        }

        public ImageViewModel ToViewModel(Image model)
        {
            return new ImageViewModel
            {
                Name = model.Name,
                Imagecode = model.Imagecode,
                File = model.File,
            };
        }
    }
}
