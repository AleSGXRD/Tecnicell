import { Branch } from "./Branch";
import { PhoneBrand } from "./PhoneBrand";
import { Sale } from "./Sale";

export interface PhoneRepair {
    imei:                 string;
    brand:                string;
    customerName:         string;
    customerID:           string;
    customerNumber:       string;
    price:                number;
    brandNavigation:      PhoneBrand;
    phoneRepairHistories: PhoneRepairHistory[];
}

export interface PhoneRepairHistory {
    imei:               string;
    date:               Date;
    actionHistory:      string;
    toBranch:           string;
    description:        string;
    saleCode:           string;
    saleCodeNavigation: Sale;
    toBranchNavigation: Branch;
}
