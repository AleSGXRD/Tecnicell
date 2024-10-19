export interface TableField{
    type : TableFieldType,
    propertyName : string,
    subPropertyName? : string,
    show: boolean,
    link? : {
        url: string,
        idPropertyName: string,
    }
    cases? : Values[]
}
export interface Values{
    key: string,
    value:any,
}
export enum TableFieldType{
    Property = "Property",
    Date = "Date",
    Link = "Link",
    Select = "Select"
}