export interface TableField{
    type : TableFieldType,
    propertyName : string,
    show: boolean,
    link? : {
        url: string,
        idPropertyName: string,
    }
}
export enum TableFieldType{
    Property = "Property",
    Link = "Link"
}