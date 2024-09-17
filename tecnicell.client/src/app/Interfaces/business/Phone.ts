import { Branch } from "./Branch";
import { PhoneBrand } from "./PhoneBrand";
import { Sale } from "./Sale";

export interface Phone {
    imei:            string;
    brand:           string;
    salePrice:       number;
    brandNavigation: PhoneBrand;
    phoneHistories:  PhoneHistory[];
}

export interface PhoneHistory {
    imei:               string;
    date:               Date;
    actionHistory:      string;
    toBranch:           string;
    description:        string;
    saleCode:           string;
    saleCodeNavigation: Sale;
    toBranchNavigation: Branch;
}