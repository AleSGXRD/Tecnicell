import { FormGroup } from "@angular/forms";
import { FormField } from "./FormField";

export interface Form {
    formGroup : FormGroup,
    inputFormFields : FormField[]
}