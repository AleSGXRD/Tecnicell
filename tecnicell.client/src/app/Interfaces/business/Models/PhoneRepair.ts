import { Branch } from "./Branch";
import { Brand } from "./Brand";
import { Sale } from "./Sale";
import { UserInfo } from "./UserAccount";

export interface PhoneRepair {
    imei?:                 string;
    name?:                 string;
    brand?:                string;
    customerName:         string;
    customerId:           string;
    customerNumber:       string;
    price?:                number;
    brandNavigation?:      Brand;
    phoneRepairHistories?: PhoneRepairHistory[];
}

export interface PhoneRepairHistory {
    imei?:               string;
    userCode:           string;
    date:               Date;
    actionHistory:      string;
    toBranch:           string;
    description:        string;
    saleCode?:           string;
    saleCodeNavigation?: Sale;
    toBranchNavigation?: Branch;
    userCodeNavigation?: UserInfo;
}
