export interface HeaderField{
    name:string;
    space:SpacesField;
}

export enum SpacesField {
    small = 'small',
    normal = 'normal',
    big = 'big'
}