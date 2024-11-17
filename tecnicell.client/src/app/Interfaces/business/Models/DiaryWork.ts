import { Sale } from "./Sale";
import { UserInfo } from "./UserAccount";
import { WorkType } from "./WorkType";

export interface DiaryWork{
    date :        Date;
    workType :    string;
    description? :string;
    saleCode?:    string;
    userCode?:    string;
    workTypeNavigation?:  WorkType;
    saleCodeNavigation?:  Sale;
    userCodeNavigation?:  UserInfo;
}