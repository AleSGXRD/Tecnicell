import { Image } from "../Models/Image";
import { Phone, PhoneHistory } from "../Models/Phone";

export interface PhoneRequest {
    model:      Phone;
    image?:     Image;
    history: PhoneHistory;
}