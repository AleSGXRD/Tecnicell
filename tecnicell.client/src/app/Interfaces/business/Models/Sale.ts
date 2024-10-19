import { Currency } from "./Currency";

export interface Sale {
    saleCode?:               string;
    currencyCode?:           string;
    warranty?:               Date;
    cost?:                   number;
    currencyCodeNavigation?: Currency;
}
export interface SaleViewModel {
    saleCode?:               string;
    currencyCode?:           string;
    warranty?:               Date;
    cost?:                   number;
    currencyViewModel?: Currency;
}