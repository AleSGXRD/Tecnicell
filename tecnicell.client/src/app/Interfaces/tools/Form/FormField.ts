
export interface FormField{
    //Indicar el tipo de input
    type: string;
    //Indicar a que propiedad del form se refiere el input
    formControlName: string;
    //Nombre del input
    name:string;
    //PlaceHolder
    placeholder: any;
}