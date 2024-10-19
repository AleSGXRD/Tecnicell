import { AccessoryHistory } from "../Models/Accessory";
import { Image } from "../Models/Image";

export interface AccessoryResponse {
    view:      AccessoryView;
    image:     Image;
    histories: AccessoryHistory[];
}
export interface AccessoryView {
    code:      string;
    type:      string;
    typeCode: string;
    name:      string;
    salePrice: number;
    imageCode: string;
    quantity:  number;
    available: string;
}
