import { Branch } from "./Branch";
import { Brand } from "./Brand";
import { Sale } from "./Sale";
import { Supplier } from "./Supplier";
import { UserInfo } from "./UserAccount";

export interface Screen {
    screenCode:      string;
    brand:           string;
    name:            string;
    width?:           number;
    height?:          number;
    warranty:         number;
    salePrice:       number;
    brandNavigation?: Brand;
    screenHistories?: ScreenHistory[];
}
export interface ScreenHistory {
    screenCode?:         string;
    userCode:           string;
    date:               Date;
    actionHistory:      string;
    toBranch?:           string;
    description?:        string;
    quantity:           number;
    saleCode?:           string;
    supplierCode?:       string;
    supplierNavigation?: Supplier;
    saleCodeNavigation?: Sale;
    toBranchNavigation?: Branch;
    userCodeNavigation?: UserInfo;
}
