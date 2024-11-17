import { Branch } from "./Branch";
import { Brand } from "./Brand";
import { Sale } from "./Sale";
import { Supplier } from "./Supplier";
import { UserInfo } from "./UserAccount";

export interface Battery {
    batteryCode:      string;
    name:             string;
    brand:            string;
    salePrice:        number;
    warranty:         number;
    batteryHistories?: BatteryHistory[];
    brandNavigation?:  Brand;
}

export interface BatteryHistory {
    batteryCode?:        string;
    userCode:           string;
    date:               Date;
    actionHistory:      string;
    toBranch?:           string;
    description:        string;
    quantity:           number;
    saleCode?:           string;
    supplierCode?:       string;
    supplierNavigation?: Supplier;
    saleCodeNavigation?: Sale;
    toBranchNavigation?: Branch;
    userCodeNavigation?: UserInfo;
}

