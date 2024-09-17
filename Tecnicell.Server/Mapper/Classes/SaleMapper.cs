using Tecnicell.Server.Models.Entity;
using Tecnicell.Server.Models.ViewModel;

namespace Tecnicell.Server.Mapper.Classes
{
    public class SaleMapper : IMapper<Sale, SaleViewModel>
    {
        public Sale ToModel(SaleViewModel viewmodel)
        {
            CurrencyMapper currencyMapper = new CurrencyMapper();
            Sale model = new Sale
            {
                SaleCode = viewmodel.SaleCode,
                Cost = viewmodel.Cost,
                CurrencyCode = viewmodel.CurrencyCode,
                Warranty = viewmodel.Warranty,
            };
            if (viewmodel.CurrencyCodeNavigation != null) model.CurrencyCodeNavigation = currencyMapper.ToModel(viewmodel.CurrencyCodeNavigation);

            return model;
        }

        public SaleViewModel ToViewModel(Sale model)
        {
            CurrencyMapper currencyMapper = new CurrencyMapper();
            SaleViewModel viewmodel = new SaleViewModel
            {
                SaleCode = model.SaleCode,
                Cost = model.Cost,
                CurrencyCode = model.CurrencyCode,
                Warranty = model.Warranty
            };
            if (model.CurrencyCodeNavigation != null) viewmodel.CurrencyCodeNavigation = currencyMapper.ToViewModel(model.CurrencyCodeNavigation);

            return viewmodel;
        }
    }
}
