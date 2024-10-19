import { Image } from "../Models/Image";
import { PhoneRepair, PhoneRepairHistory } from "../Models/PhoneRepair";

export interface PhoneRepairRequest {
    model:      PhoneRepair;
    image?:     Image;
    history: PhoneRepairHistory;
}