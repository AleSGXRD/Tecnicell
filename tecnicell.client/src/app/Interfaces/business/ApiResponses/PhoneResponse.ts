import { Image } from "../Models/Image";
import { PhoneHistory } from "../Models/Phone";

export interface PhoneResponse {
    view:      PhoneView;
    image:     Image;
    histories: PhoneHistory[];
}

export interface PhoneView {
    code:              string;
    type:              string;
    name:              string;
    salePrice:         number;
    imageCode:         string;
    cost:              number;
    currentState:      string;
    actionDescription: string;
}