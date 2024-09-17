import { Currency } from "./Currency";

export interface Sale {
    saleCode:               string;
    currencyCode:           string;
    warranty:               Date;
    cost:                   number;
    currencyCodeNavigation: Currency;
}