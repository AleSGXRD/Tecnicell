import { Component, Input } from '@angular/core';
import { FormField } from '../../Interfaces/tools/Form/FormField';
import { ErrorStateMatcher } from '@angular/material/core';

@Component({
  selector: 'app-form-field-conditioner',
  templateUrl: './form-field-conditioner.component.html',
  styleUrl: './form-field-conditioner.component.css'
})
export class FormFieldConditionerComponent {
  @Input()
  formField!: FormField;
  @Input()
  matcher!: ErrorStateMatcher;
  @Input()
  form! : any;

  checked = false;
}
