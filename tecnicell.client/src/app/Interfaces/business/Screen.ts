import { Branch } from "./Branch";
import { PhoneBrand } from "./PhoneBrand";
import { Sale } from "./Sale";

export interface Screen {
    screenCode:      string;
    brand:           string;
    name:            string;
    quantity:        number;
    width:           number;
    height:          number;
    salePrice:       number;
    warranty:        number;
    brandNavigation: PhoneBrand;
    screenHistories: ScreenHistory[];
}
export interface ScreenHistory {
    screenCode:         string;
    date:               Date;
    actionHistory:      string;
    toBranch:           string;
    description:        string;
    quantity:           number;
    saleCode:           string;
    saleCodeNavigation: Sale;
    toBranchNavigation: Branch;
}
