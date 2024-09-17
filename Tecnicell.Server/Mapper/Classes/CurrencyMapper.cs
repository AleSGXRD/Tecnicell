using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class CurrencyMapper : IMapper<Currency, CurrencyViewModel>
    {
        public Currency ToModel(CurrencyViewModel viewmodel)
        {
            return new Currency
            {
                CurrencyCode = viewmodel.CurrencyCode,
                CurrencyName = viewmodel.CurrencyName,
            };
        }

        public CurrencyViewModel ToViewModel(Currency model)
        {
            return new CurrencyViewModel
            {
                CurrencyCode = model.CurrencyCode,
                CurrencyName = model.CurrencyName,
            };
        }
    }
}
