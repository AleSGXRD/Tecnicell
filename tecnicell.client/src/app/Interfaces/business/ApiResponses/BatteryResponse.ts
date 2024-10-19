import { BatteryHistory } from "../Models/Battery"
import { Image } from "../Models/Image";

export interface BatteryResponse {
    view:      BatteryView;
    image?:     Image;
    histories: BatteryHistory[];
}
export interface BatteryView {
    code:      string;
    type:      string;
    name:      string;
    salePrice: number;
    warranty:number;
    imageCode: string;
    quantity:  number;
    available: string;
}
