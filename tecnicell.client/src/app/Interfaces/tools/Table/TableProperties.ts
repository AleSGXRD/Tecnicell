import { HeaderField } from "./HeaderField";
import { LinkField } from "./LinkField";
import { TableField } from "./TableField";

export interface TableProperties{
    values : any[],
    headerFields : HeaderField[],
    tableFields : TableField[],
    linkFields? :  LinkField
}