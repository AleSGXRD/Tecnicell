import { UserResponse } from "./UserResponse";

export interface Response{
    success :number,
    message ? : string,
    user : UserResponse
}