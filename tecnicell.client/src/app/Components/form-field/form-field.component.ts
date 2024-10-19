import { Component, Input, signal, SimpleChanges } from '@angular/core';
import { FormField } from '../../Interfaces/tools/Form/FormField';
import { ErrorStateMatcher } from '@angular/material/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-form-field',
  templateUrl: './form-field.component.html',
  styleUrl: './form-field.component.css'
})
export class FormFieldComponent {
  @Input()
  formField!: FormField;
  @Input()
  matcher!: ErrorStateMatcher;
  @Input()
  form! : any;

  checked = false;

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }
}
