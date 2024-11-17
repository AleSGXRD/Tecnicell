import { Branch } from "./Branch";
import { Sale } from "./Sale";
import { Supplier } from "./Supplier";
import { UserInfo } from "./UserAccount";

export interface Accessory {
    accessoryCode?:           string;
    name:                    string;
    accessoryType:           string;
    salePrice:               number;
    accessoryHistories?:      AccessoryHistory[];
    accessoryTypeNavigation?: AccessoryType;
}

export interface AccessoryHistory {
    accessoryCode?:      string;
    userCode:           string;
    date:               Date;
    actionHistory:      string;
    toBranch?:           string;
    description:        string;
    quantity:           number;
    saleCode?:           string;
    supplierCode?:       string;
    supplierCodeNavigation?: Supplier;
    saleCodeNavigation?: Sale;
    toBranchNavigation?: Branch;
    userCodeNavigation?: UserInfo;
}

export interface AccessoryType {
    accessoryTypeCode: string;
    name:              string;
}
