import { Battery, BatteryHistory } from "../Models/Battery"
import { Image } from "../Models/Image";

export interface BatteryRequest {
    model:      Battery;
    image?:     Image;
    history: BatteryHistory;
}
