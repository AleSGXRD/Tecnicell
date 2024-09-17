import { Branch } from "./Branch";
import { Currency } from "./Currency";
import { Sale } from "./Sale";

export interface Accessory {
    accessoryCode:           string;
    name:                    string;
    accessoryType:           string;
    salePrice:               number;
    quantity:                number;
    accessoryHistories:      AccessoryHistory[];
    accessoryTypeNavigation: AccessoryType;
}

export interface AccessoryHistory {
    accessoryCode:      string;
    date:               Date;
    actionHistory:      string;
    toBranch:           string;
    description:        string;
    quantity:           number;
    saleCode:           string;
    saleCodeNavigation: Sale;
    toBranchNavigation: Branch;
}

export interface AccessoryType {
    accessoryTypeCode: string;
    name:              string;
}
