import { Image } from "./Image";
import { Role } from "./Role";

export interface UserInfo{
    userCode? : string,
    imageCode? : string,
    name? : string,
    role : string,
    branch :string,
    roleNavigation? : Role,
    imageCodeNavigation? : Image
}

export interface UserAccount{
    userCode? : string,
    name :string,
    password :string
}