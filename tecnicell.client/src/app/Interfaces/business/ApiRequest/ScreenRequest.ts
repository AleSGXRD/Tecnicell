import { Screen, ScreenHistory } from "../Models/Screen"
import { Image } from "../Models/Image";

export interface ScreenRequest {
    model:      Screen;
    image?:     Image;
    history: ScreenHistory;
}