import { HeaderField } from "./HeaderField";
import { TableField } from "./TableField";

export interface TableProperties{
    values : any[],
    headerFields : HeaderField[],
    tableFields : TableField[]
}