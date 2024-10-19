import { Image } from "../Models/Image";
import { PhoneRepairHistory } from "../Models/PhoneRepair";

export interface PhoneRepairResponse {
    view:      PhoneRepairView;
    image:     Image;
    histories: PhoneRepairHistory[];
}

export interface PhoneRepairView {
    code:              string;
    type:              string;
    name:              string;
    customerName:      string;
    customerId:        string;
    customerNumber:    string;
    price:             number;
    imageCode:         string;
    date:              Date;
    currentState:      string;
    actionDescription: string;
}