import { ValidationErrors } from "@angular/forms";

export interface FormField{
    //Indicar el tipo de input
    type: string;
    //Indicar a que propiedad del form se refiere el input
    formControlName: string;
    //Nombre del input
    name:string;
    //PlaceHolder
    placeholder: any;
    fieldRequired : boolean;
    disabled? : boolean;

    condition? : FormFieldCondition;

    errors? : FormFieldError[];
    options? : FormFieldOption[];
    fields? : FormField[];
}
export interface FormFieldError{
    type : string,
    message : string,
}
export interface FormFieldOption{
    value:any,
    name:string
}
export interface FormFieldCondition{
    formControlName: string;
    value:any;
}