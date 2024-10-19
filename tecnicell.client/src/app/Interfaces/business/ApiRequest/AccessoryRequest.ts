import { Accessory, AccessoryHistory } from "../Models/Accessory";
import { Image } from "../Models/Image";

export interface AccessoryRequest {
    model:      Accessory;
    image?:     Image;
    history: AccessoryHistory;
}
