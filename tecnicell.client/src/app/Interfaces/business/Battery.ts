import { Branch } from "./Branch";
import { Sale } from "./Sale";

export interface Battery {
    batteryCode:      string;
    name:             string;
    brand:            string;
    quantity:         number;
    salePrice:        number;
    warranty:         number;
    batteryHistories: BatteryHistory[];
    brandNavigation:  BatteryBrand;
}

export interface BatteryHistory {
    batteryCode:        string;
    date:               Date;
    actionHistory:      string;
    toBranch:           string;
    description:        string;
    quantity:           number;
    saleCode:           string;
    saleCodeNavigation: Sale;
    toBranchNavigation: Branch;
}

export interface BatteryBrand {
    name:        string;
    description: string;
}
