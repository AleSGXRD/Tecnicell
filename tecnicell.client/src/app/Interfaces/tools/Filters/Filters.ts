import { FormFieldOption } from "../Form/FormField";
import { Values } from "../Table/TableField";

export interface FilterField{
    name:string,
    type: FilterType,
    save?: boolean,
    propertyName : string,
    otherProperties? : [{
        propertyName:string,
        subPropertyName?:string
    }]
    value? : any,
    options? : FormFieldOption[],
    cases? : Values[]
}
export enum FilterType{
    DATE = 'date',
    TEXT = 'text',
    NUMBER = 'number',
    SELECT = 'select'
}