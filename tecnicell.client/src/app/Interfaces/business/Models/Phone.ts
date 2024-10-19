import { Branch } from "./Branch";
import { Brand } from "./Brand";
import { Sale } from "./Sale";
import { UserInfo } from "./UserAccount";

export interface Phone {
    imei:            string;
    name?:            string;
    brand:           string;
    salePrice:       number;
    brandNavigation?: Brand;
    phoneHistories?:  PhoneHistory[];
}

export interface PhoneHistory {
    imei?:               string;
    userCode:            string;
    date:                Date;
    actionHistory:       string;
    toBranch?:           string;
    description?:        string;
    saleCode?:           string;
    saleCodeNavigation?: Sale;
    toBranchNavigation?: Branch;
    userCodeNavigation?: UserInfo;
}