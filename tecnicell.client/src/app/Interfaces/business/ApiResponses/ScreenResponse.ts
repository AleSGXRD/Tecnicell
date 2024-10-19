import { ScreenHistory } from "../Models/Screen"
import { Image } from "../Models/Image";

export interface ScreenResponse {
    view:      ScreenView;
    image:     Image;
    histories: ScreenHistory[];
}
export interface ScreenView {
    code:      string;
    type:      string;
    name:      string;
    salePrice: number;
    imageCode: string;
    quantity:  number;
    warranty:  number;
    available: string;
}
